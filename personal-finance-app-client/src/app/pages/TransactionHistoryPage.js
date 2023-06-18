import {
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  Slider,
} from "@mui/material";
import { useState } from "react";
import TransactionList from "../components/transactions/TransactionList";

function TransactionHistoryPage() {
  const [startDate, setStartDate] = useState(null);
  const [range, setRange] = useState(5);
  const [type, setType] = useState("all");
  const [income, setIncome] = useState(0);
  const [expenditure, setExpenditure] = useState(0);
  const [min, setMin] = useState();
  const [max, setMax] = useState();
  const [amount, setAmount] = useState([0, Number.MAX_SAFE_INTEGER]);

  const handleRange = (e) => {
    var input = e.target.value;
    var now = new Date();
    var newDate = new Date();
    if (input === 2) {
      newDate = new Date(now.getFullYear(), now.getMonth(), now.getDate() - 7);
    }
    if (input === 3) {
      newDate = new Date(now.getFullYear(), now.getMonth(), 1);
    }
    if (input === 4) {
      newDate = new Date(now.getFullYear(), 0, 1);
    }
    if (input === 5) {
      newDate = null;
    }
    setStartDate(newDate);
    setRange(e.target.value);
  };
  return (
    <div className="pt-4 text-center">
      <p className="display-6">Transaction history</p>
      <div className="text-start py-3">
        <p>
          Income for the selected time period:
          <b className="text-success"> +${income}</b>
        </p>
        <p>
          Expenditure for the selected time period:
          <b className="text-danger"> -${expenditure}</b>
        </p>
        <p>
          Balance:
          {income - expenditure > 0 ? (
            <b className="text-success">
              {" "}
              +${Math.abs(income - expenditure).toFixed(2)}
            </b>
          ) : (
            <>
              {income - expenditure < 0 ? (
                <b className="text-danger">
                  {" "}
                  -${Math.abs(income - expenditure).toFixed(2)}
                </b>
              ) : (
                <b> $0</b>
              )}
            </>
          )}
        </p>
        <div className="py-3">
          <FormControl className="w-50">
            <InputLabel>Time period</InputLabel>
            <Select value={range} label="Time period" onChange={handleRange}>
              <MenuItem value={1}>Today</MenuItem>
              <MenuItem value={2}>Last 7 days</MenuItem>
              <MenuItem value={3}>This month</MenuItem>
              <MenuItem value={4}>This year</MenuItem>
              <MenuItem value={5}>Entire period</MenuItem>
            </Select>
          </FormControl>
        </div>
        <div>
          <FormControl className="w-50">
            <InputLabel>Transactions type</InputLabel>
            <Select
              value={type}
              label="Transactions type"
              onChange={(e) => setType(e.target.value)}
            >
              <MenuItem value="all">All</MenuItem>
              <MenuItem value="income">Incomes</MenuItem>
              <MenuItem value="expenditure">Expenditures</MenuItem>
            </Select>
          </FormControl>
        </div>
        <div className="py-3">
          <InputLabel className="my-3 text-dark">Amount range</InputLabel>
          <Slider
            className="w-50"
            label="amount range"
            getAriaLabel={() => "Temperature range"}
            value={amount}
            max={max}
            min={min}
            onChange={(e) => setAmount(e.target.value)}
            valueLabelDisplay="auto"
          />
        </div>
      </div>
      <TransactionList
        startDate={startDate}
        setIncome={setIncome}
        setExpenditure={setExpenditure}
        type={type}
        setMax={setMax}
        setMin={setMin}
        amount={amount}
      />
    </div>
  );
}

export default TransactionHistoryPage;
