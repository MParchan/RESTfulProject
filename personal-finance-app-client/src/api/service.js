import axiosInstance from "./axiosInstance";
import { store } from "../app/store";

export const getUserExpenditureCategories = async (email) => {
  const state = store.getState().auth;
  axiosInstance.defaults.headers.common["Authorization"] =
    "Bearer " + state.accessToken;
  const bodyParameters = {
    params: { email },
  };
  const response = await axiosInstance.get(
    "/ExpenditureCategories/UserExpenditureCategories",
    bodyParameters
  );
  return response.data;
};

export const getUserIncomeCategories = async (email) => {
  const state = store.getState().auth;
  axiosInstance.defaults.headers.common["Authorization"] =
    "Bearer " + state.accessToken;
  const bodyParameters = {
    params: { email },
  };
  const response = await axiosInstance.get(
    "/IncomeCategories/UserIncomeCategories",
    bodyParameters
  );
  return response.data;
};

export const addExpenditure = async (email, expenditure) => {
  const state = store.getState().auth;
  axiosInstance.defaults.headers.common["Authorization"] =
    "Bearer " + state.accessToken;
  const bodyParameters = {
    params: { email },
  };
  return await axiosInstance.post("/Expenditures", expenditure, bodyParameters);
};

export const addIncome = async (email, income) => {
  const state = store.getState().auth;
  axiosInstance.defaults.headers.common["Authorization"] =
    "Bearer " + state.accessToken;
  const bodyParameters = {
    params: { email },
  };
  return await axiosInstance.post("/Incomes", income, bodyParameters);
};

export const getUserExpenditures = async (email) => {
  const state = store.getState().auth;
  axiosInstance.defaults.headers.common["Authorization"] =
    "Bearer " + state.accessToken;
  const bodyParameters = {
    params: { email },
  };
  return await axiosInstance.get(
    "/Expenditures/UserExpenditures",
    bodyParameters
  );
};

export const getUserIncomes = async (email) => {
  const state = store.getState().auth;
  axiosInstance.defaults.headers.common["Authorization"] =
    "Bearer " + state.accessToken;
  const bodyParameters = {
    params: { email },
  };
  return await axiosInstance.get("/Incomes/UserIncomes", bodyParameters);
};

export const addIncomeCategorie = async (email, newCategory) => {
  const state = store.getState().auth;
  axiosInstance.defaults.headers.common["Authorization"] =
    "Bearer " + state.accessToken;
  const bodyParameters = {
    params: { email },
  };
  return await axiosInstance
    .post("/IncomeCategories", newCategory, bodyParameters)
    .then((response) => {
      return response;
    });
};

export const addExpenditureCategorie = async (email, newCategory) => {
  const state = store.getState().auth;
  axiosInstance.defaults.headers.common["Authorization"] =
    "Bearer " + state.accessToken;
  const bodyParameters = {
    params: { email },
  };
  return await axiosInstance
    .post("/ExpenditureCategories", newCategory, bodyParameters)
    .then((response) => {
      return response;
    });
};
