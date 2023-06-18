import { createSlice } from "@reduxjs/toolkit";
import {
  userLogin,
  userLogout,
  userRefreshToken,
  userRegistration,
} from "./authActions";

const initialState = {
  loading: false,
  userEmail: null,
  accessToken: null,
  refreshToken: null,
  registerError: null,
  registerSuccess: false,
  loginError: null,
  logged: false,
};

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    resetAuth(state) {
      state.loading = false;
      state.userEmail = null;
      state.accessToken = null;
      state.refreshToken = null;
      state.loginError = null;
      state.registerError = null;
      state.registerSuccess = false;
      state.logged = false;
    },
  },
  extraReducers: (builder) => {
    builder
      //registration
      .addCase(userRegistration.pending, (state) => {
        state.loading = true;
        state.registerError = null;
      })
      .addCase(userRegistration.fulfilled, (state) => {
        state.loading = false;
        state.registerSuccess = true;
      })
      .addCase(userRegistration.rejected, (state, { payload }) => {
        state.loading = false;
        state.registerError = payload;
      })

      //login
      .addCase(userLogin.pending, (state) => {
        state.loading = true;
        state.loginError = null;
      })
      .addCase(userLogin.fulfilled, (state, { payload }) => {
        state.logged = true;
        state.userEmail = payload.email;
        state.accessToken = payload.accessToken;
        state.refreshToken = payload.refreshToken;
        state.loading = false;
      })
      .addCase(userLogin.rejected, (state, { payload }) => {
        state.loading = false;
        state.loginError = payload;
      })

      //refresh token
      .addCase(userRefreshToken.pending, (state) => {
        state.loading = true;
        state.loginError = null;
      })
      .addCase(userRefreshToken.fulfilled, (state, { payload }) => {
        state.loading = false;
        state.logged = true;
        state.accessToken = payload.accessToken;
        state.refreshToken = payload.refreshToken;
      })
      .addCase(userRefreshToken.rejected, (state, { payload }) => {
        state.loading = false;
        state.loginError = payload;
      })

      //logout
      .addCase(userLogout.pending, (state) => {
        state.loading = true;
      })
      .addCase(userLogout.fulfilled, (state) => {
        state.userEmail = null;
        state.accessToken = null;
        state.refreshToken = null;
        state.logged = false;
        state.loading = false;
      })
      .addCase(userLogout.rejected, (state) => {
        state.loading = false;
      })

      .addDefaultCase(() => {});
  },
});

export const { resetAuth } = authSlice.actions;
export default authSlice.reducer;
