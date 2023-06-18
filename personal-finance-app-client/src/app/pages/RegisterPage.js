import { CircularProgress, TextField } from "@mui/material";
import { useDispatch, useSelector } from "react-redux";
import { useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { useEffect } from "react";
import { userRegistration } from "../../features/auth/authActions";
import { resetAuth } from "../../features/auth/authSlice";

function RegisterPage() {
  const { loading, registerError, registerSuccess } = useSelector(
    (state) => state.auth
  );
  const dispatch = useDispatch();
  const { register, handleSubmit } = useForm();
  const navigate = useNavigate();

  useEffect(() => {
    dispatch(resetAuth());
  }, [dispatch]);

  useEffect(() => {
    if (registerSuccess && !loading) {
      navigate("/login");
    }
  }, [navigate, registerSuccess, loading]);

  const handleRegistration = (data) => {
    dispatch(userRegistration(data));
  };

  return (
    <div className="text-center">
      <form onSubmit={handleSubmit(handleRegistration)}>
        <p className="m-5 display-5">Registration</p>
        <div className="m-3">
          <TextField
            type="email"
            label="Email"
            className="w-50"
            {...register("email")}
            required
          />
        </div>
        <div className="m-3">
          <TextField
            type="password"
            label="Password"
            className="w-50"
            inputProps={{ minLength: 8 }}
            {...register("password")}
            required
          />
        </div>
        <div className="m-3">
          <TextField
            type="password"
            label="Confirm password"
            className="w-50"
            {...register("confirmPassword")}
            required
          />
        </div>
        <div className="form-group m-3">
          <span className="text-danger">{registerError}</span>
        </div>
        <button type="submit" className="btn btn-dark w-25" disabled={loading}>
          {loading ? (
            <CircularProgress color="inherit" size={20} />
          ) : (
            "Register"
          )}
        </button>
      </form>
    </div>
  );
}

export default RegisterPage;
