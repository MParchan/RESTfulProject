import {
  Alert,
  CircularProgress,
  Dialog,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  Snackbar,
  TextField,
} from "@mui/material";
import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { addIncome, getUserIncomeCategories } from "../../../api/service";
import { AddIncomeCategoryForm } from "./AddIncomeCategoryForm";

function AddIncomeForm(props) {
  const { userEmail } = useSelector((state) => state.auth);
  const [userIncomecategories, setUserIncomeCategories] = useState([]);
  const [incomeCategory, setIncomeCategory] = useState("");
  const [comment, setComment] = useState("");
  const [amount, setAmount] = useState("");
  const [openSnackSuccess, setOpenSnackSuccess] = useState(false);
  const [openSnackError, setOpenSnackError] = useState(false);
  const [loading, setLoading] = useState(true);
  const [openCategory, setOpenCategory] = useState(false);

  useEffect(() => {
    getUserIncomeCategories(userEmail).then((data) => {
      const categories = [];
      for (const key in data) {
        const category = {
          id: key,
          ...data[key],
        };
        categories.push(category);
      }
      setUserIncomeCategories(categories);
      setLoading(false);
    });
  }, [openCategory, userEmail]);

  const handleForm = (e) => {
    e.preventDefault();
    const newIncome = {
      amount: amount,
      incomeCategoryId: incomeCategory,
      comment: comment,
      userId: -1,
    };
    addIncome(userEmail, newIncome)
      .then((response) => {
        if (response.status === 201) {
          setAmount("");
          setIncomeCategory("");
          setComment("");
          setOpenSnackSuccess(true);
          props.setReload(true);
        } else {
          setOpenSnackError(true);
        }
      })
      .catch(() => {
        setOpenSnackError(true);
      });
  };

  const handleCloseCategory = (e) => {
    setOpenCategory(false);
  };

  const handleCloseSnackSuccess = () => {
    setOpenSnackSuccess(false);
  };

  const handleCloseSnackError = () => {
    setOpenSnackError(false);
  };

  const handleAmount = (e) => {
    var val = e.target.value;
    var decimal = "";
    if (val.toString().includes(".")) {
      decimal = val.toString().split(".")[1];
    }
    if (decimal.length <= 2) {
      setAmount(val);
    }
  };

  if (loading) {
    return (
      <div className="text-center">
        <CircularProgress color="inherit" />
      </div>
    );
  }

  return (
    <div>
      <form className="text-center" onSubmit={handleForm}>
        <p className="display-6 m-0">Add</p>
        <p className="display-6">income</p>
        <div className="my-2">
          <TextField
            fullWidth
            value={amount}
            type="number"
            label="Amount of income"
            inputProps={{
              step: "0.01",
              min: 0,
            }}
            onChange={handleAmount}
            required
          />
        </div>
        <div className="my-2">
          <FormControl fullWidth>
            <InputLabel>Income category *</InputLabel>
            <Select
              label="Income category"
              value={incomeCategory}
              onChange={(e) => setIncomeCategory(e.target.value)}
              required
            >
              <MenuItem value="" onClick={(e) => setOpenCategory(true)}>
                <em>+ Add category</em>
              </MenuItem>
              {userIncomecategories.map((category) => (
                <MenuItem key={category.id} value={category.incomeCategoryId}>
                  {category.name}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </div>
        <div className="my-2">
          <TextField
            fullWidth
            type="text"
            value={comment}
            multiline={true}
            rows={3}
            label="Comment"
            inputProps={{ maxLength: 60 }}
            onChange={(e) => setComment(e.target.value)}
          />
        </div>
        <button type="submit" className="btn btn-dark">
          Add
        </button>
        <Snackbar
          open={openSnackSuccess}
          autoHideDuration={3000}
          onClose={handleCloseSnackSuccess}
        >
          <Alert severity="success" sx={{ width: "100%" }}>
            Income added successfully!
          </Alert>
        </Snackbar>
        <Snackbar
          open={openSnackError}
          autoHideDuration={3000}
          onClose={handleCloseSnackError}
        >
          <Alert severity="error" sx={{ width: "100%" }}>
            Operation failed!
          </Alert>
        </Snackbar>
      </form>
      <Dialog open={openCategory} onClose={handleCloseCategory}>
        <AddIncomeCategoryForm handleCloseCategory={handleCloseCategory} />
      </Dialog>
    </div>
  );
}

export default AddIncomeForm;
