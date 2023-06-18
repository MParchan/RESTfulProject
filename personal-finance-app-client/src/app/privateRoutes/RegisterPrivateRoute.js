import { useSelector } from "react-redux";
import { Navigate } from "react-router-dom";

const RegisterPrivateRoute = ({ children }) => {
  const { logged } = useSelector((state) => state.auth);
  return logged ? <Navigate to={"/"} /> : children;
};

export default RegisterPrivateRoute;
