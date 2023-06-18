import { useSelector } from "react-redux";
import { Navigate } from "react-router-dom";

const LoginPrivateRoute = ({ children }) => {
  const { logged } = useSelector((state) => state.auth);
  return logged ? <Navigate to={"/"} /> : children;
};

export default LoginPrivateRoute;
