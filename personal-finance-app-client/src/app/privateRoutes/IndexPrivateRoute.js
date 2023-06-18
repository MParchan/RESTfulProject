import { useSelector } from "react-redux";
import IndexLoggedPage from "../pages/IndexLoggedPage";
import IndexPage from "../pages/IndexPage";

const IndexPrivateRoute = () => {
  const { logged } = useSelector((state) => state.auth);
  return logged ? <IndexLoggedPage /> : <IndexPage />;
};

export default IndexPrivateRoute;
