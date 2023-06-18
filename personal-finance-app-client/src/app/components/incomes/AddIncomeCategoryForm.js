import { TextField } from "@mui/material";
import { useState } from "react";
import { useSelector } from "react-redux";
import { addIncomeCategorie } from "../../../api/service";

export const AddIncomeCategoryForm = ({ handleCloseCategory }) => {
  const [name, setName] = useState("");
  const { userEmail } = useSelector((state) => state.auth);

  const handleForm = async (e) => {
    e.preventDefault();
    const newCategory = {
      name: name,
    };
    const response = await addIncomeCategorie(userEmail, newCategory);
    if (response.status === 201) {
      handleCloseCategory();
    }
  };
  return (
    <div className="p-3">
      <h1>Add category</h1>
      <form onSubmit={handleForm}>
        <div className="my-2">
          <TextField
            type="text"
            label="Name"
            className="w-100"
            onChange={(e) => setName(e.target.value)}
            required
          />
        </div>
        <button type="submit" className="btn btn-dark">
          Add category
        </button>
      </form>
    </div>
  );
};
