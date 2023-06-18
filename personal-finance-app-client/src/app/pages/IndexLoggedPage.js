import { Paper } from "@mui/material";
import { useEffect, useState } from "react";
import AddExpenditureForm from "../components/expenditures/AddExpenditureForm";
import AddIncomeForm from "../components/incomes/AddIncomeForm";
import TransactionsChart from "../components/transactions/TransactionsChart";

function IndexLoggedPage() {
  const [reload, setReload] = useState(false);
  useEffect(() => {
    setReload(false);
  }, [reload]);
  return (
    <div className="pt-4">
      <Paper className="p-3 my-3">
        <div className="row">
          <div className="col-6">
            <AddIncomeForm setReload={setReload} />
          </div>
          <div className="col-6">
            <AddExpenditureForm setReload={setReload} />
          </div>
        </div>
      </Paper>
      <Paper className="p-3 my-5">
        <TransactionsChart reload={reload} />
      </Paper>
    </div>
  );
}

export default IndexLoggedPage;
