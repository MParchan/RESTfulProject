import axios from "axios";
import { userLogout, userRefreshToken } from "../features/auth/authActions";
import { store } from "../app/store";
import { Navigate } from "react-router-dom";

const baseURL = "https://localhost:7190/api";

let isRefreshing = false;
let failedQueue = [];

export const axiosInstance = axios.create({
  baseURL,
});

const processQueue = (error, token = null) => {
  failedQueue.forEach((prom) => {
    if (error) {
      prom.reject(error);
    } else {
      prom.resolve(token);
    }
  });

  failedQueue = [];
};

axiosInstance.interceptors.response.use(
  async (response) => {
    return response;
  },
  async (err) => {
    const originalRequest = err.config;

    if (err.response.status === 401 && !originalRequest._retry) {
      if (isRefreshing) {
        try {
          const token = await new Promise(function (resolve, reject) {
            failedQueue.push({ resolve, reject });
          });
          originalRequest.headers["Authorization"] = "Bearer " + token;
          return await axios(originalRequest);
        } catch (err_1) {
          return await Promise.reject(err_1);
        }
      }

      originalRequest._retry = true;
      isRefreshing = true;
      const state = store.getState().auth;
      const email = state.userEmail;
      const refreshToken = state.refreshToken;
      const accessToken = state.accessToken;
      axios.defaults.headers.common["Authorization"] = "Bearer " + accessToken;
      const bodyParameters = {
        params: { email, refreshToken },
      };
      return new Promise(async function (resolve, reject) {
        await axios
          .post(baseURL + "/Auth/RefreshToken", {}, bodyParameters)
          .then((response) => {
            return response.data;
          })
          .then((data) => {
            axiosInstance.defaults.headers.common["Authorization"] =
              "Bearer " + data.accessToken;
            originalRequest.headers["Authorization"] =
              "Bearer " + data.accessToken;
            const dataToken = {
              accessToken: data.accessToken,
              refreshToken: data.refreshToken,
            };
            store.dispatch(userRefreshToken(dataToken));
            processQueue(null, data.accessToken);
            resolve(axios(originalRequest));
          })
          .catch((err) => {
            delete axiosInstance.defaults.headers.common["Authorization"];
            delete axios.defaults.headers.common["Authorization"];
            store.dispatch(userLogout());
            <Navigate to={"/login"} />;
            processQueue(err, null);
            reject(err);
          })
          .then(() => {
            isRefreshing = false;
          });
      });
    }
    return Promise.reject(err);
  }
);

export default axiosInstance;
