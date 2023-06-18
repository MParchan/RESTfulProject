import "bootstrap/dist/css/bootstrap.css";
import * as React from "react";
import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import Button from "@mui/material/Button";
import IconButton from "@mui/material/IconButton";
import MenuIcon from "@mui/icons-material/Menu";
import { Link } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { userLogout } from "../../features/auth/authActions";

export default function Navbar() {
  const { logged } = useSelector((state) => state.auth);
  const dispatch = useDispatch();

  const logoutHandler = () => {
    dispatch(userLogout());
  };

  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar position="static" className="bg-dark">
        <Toolbar>
          <IconButton
            size="large"
            edge="start"
            color="inherit"
            aria-label="menu"
            sx={{ mr: 2 }}
          >
            <MenuIcon />
          </IconButton>
          <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            <Link
              to={"/"}
              style={{ color: "inherit", textDecoration: "inherit" }}
            >
              Financial app
            </Link>
          </Typography>
          {logged ? (
            <>
              <Button
                component={Link}
                to={"/transactions"}
                style={{ color: "inherit", textDecoration: "inherit" }}
              >
                Transaction history
              </Button>
              <Button
                onClick={logoutHandler}
                style={{ color: "inherit", textDecoration: "inherit" }}
              >
                Logout
              </Button>
            </>
          ) : (
            <>
              <Button
                component={Link}
                to={"/login"}
                style={{ color: "inherit", textDecoration: "inherit" }}
              >
                Login
              </Button>
              <Button
                component={Link}
                to={"/register"}
                style={{ color: "inherit", textDecoration: "inherit" }}
              >
                Register
              </Button>
            </>
          )}
        </Toolbar>
      </AppBar>
    </Box>
  );
}
