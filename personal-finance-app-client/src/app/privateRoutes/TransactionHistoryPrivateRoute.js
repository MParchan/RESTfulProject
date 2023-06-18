import { useSelector } from "react-redux";
import { Navigate } from "react-router-dom";

const TransactionHistoryPrivateRoute = ({ children }) => {
  const { logged } = useSelector((state) => state.auth);
  return logged ? children : <Navigate to={"/login"} />;
};

export default TransactionHistoryPrivateRoute;
