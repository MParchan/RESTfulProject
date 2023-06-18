import { Route, Routes } from "react-router-dom";
import LoginPage from "./pages/LoginPage";
import RegisterPage from "./pages/RegisterPage";
import TransactionHistoryPage from "./pages/TransactionHistoryPage";
import IndexPrivateRoute from "./privateRoutes/IndexPrivateRoute";
import LoginPrivateRoute from "./privateRoutes/LoginPrivateRoute";
import RegisterPrivateRoute from "./privateRoutes/RegisterPrivateRoute";
import TransactionHistoryPrivateRoute from "./privateRoutes/TransactionHistoryPrivateRoute";
import Layout from "./ui/Layout";
import Navbar from "./ui/Navbar";

function App() {
  return (
    <div>
      <Navbar />
      <Layout>
        <Routes>
          <Route path="/" element={<IndexPrivateRoute />} />
          <Route
            path="/login"
            element={
              <LoginPrivateRoute>
                <LoginPage />
              </LoginPrivateRoute>
            }
          />
          <Route
            path="/register"
            element={
              <RegisterPrivateRoute>
                <RegisterPage />
              </RegisterPrivateRoute>
            }
          />
          <Route
            path="/transactions"
            element={
              <TransactionHistoryPrivateRoute>
                <TransactionHistoryPage />
              </TransactionHistoryPrivateRoute>
            }
          />
        </Routes>
      </Layout>
    </div>
  );
}

export default App;
