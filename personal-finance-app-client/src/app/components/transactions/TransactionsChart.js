import {
  CircularProgress,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
} from "@mui/material";
import { useEffect, useState } from "react";
import { getTransactions } from "./getTransactions";
import React from "react";
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend,
} from "chart.js";
import { Bar } from "react-chartjs-2";
import { useSelector } from "react-redux";

ChartJS.register(
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend
);

const options = {
  responsive: true,
  plugins: {
    legend: {
      position: "top",
    },
    title: {
      display: true,
      text: "Incomes & Expenditures",
    },
  },
};

const labels = [
  "January",
  "February",
  "March",
  "April",
  "May",
  "June",
  "July",
  "August",
  "September",
  "October",
  "November",
  "December",
];

function TransactionsChart(props) {
  const { userEmail } = useSelector((state) => state.auth);
  const [loadedTransactions, setLoadedTransactions] = useState([]);
  const [chartData, setChartData] = useState([]);
  const [yearsList, setYearsList] = useState([]);
  const [selectedYear, setSelectedYear] = useState(new Date().getFullYear());
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const params = {
      email: userEmail,
      setLoading: setLoading,
      setLoadedTransactions: setLoadedTransactions,
    };
    getTransactions(params);
  }, [userEmail, props.reload]);

  useEffect(() => {
    var minDate = new Date();
    for (var key in loadedTransactions) {
      if (minDate > new Date(loadedTransactions[key].date)) {
        minDate = new Date(loadedTransactions[key].date);
      }
    }
    var year = minDate.getFullYear();
    const years = [];
    while (year <= new Date().getFullYear()) {
      years.push(year);
      year++;
    }
    setYearsList(years);
  }, [loadedTransactions]);

  useEffect(() => {
    const data = {
      labels,
      datasets: [
        {
          label: "Incomes",
          data: labels.map((month) => {
            const transactions = loadedTransactions.filter(
              (t) =>
                t.type === "income" &&
                new Date(t.date).getFullYear() === selectedYear &&
                labels[new Date(t.date).getMonth()] === month
            );
            var totalAmount = 0;
            for (var key in transactions) {
              totalAmount += transactions[key].amount;
            }
            return totalAmount.toFixed(2);
          }),
          backgroundColor: "rgba(0, 128, 0, 0.5)",
        },
        {
          label: "Expenditures",
          data: labels.map((month) => {
            const transactions = loadedTransactions.filter(
              (t) =>
                t.type === "expenditure" &&
                new Date(t.date).getFullYear() === selectedYear &&
                labels[new Date(t.date).getMonth()] === month
            );
            var totalAmount = 0;
            for (var key in transactions) {
              totalAmount += transactions[key].amount;
            }
            return totalAmount.toFixed(2);
          }),
          backgroundColor: "rgba(255, 0, 0, 0.5)",
        },
      ],
    };
    setChartData(data);
  }, [loadedTransactions, selectedYear]);

  if (loading) {
    return (
      <div className="text-center">
        <CircularProgress color="inherit" />
      </div>
    );
  }

  return (
    <div className="text-end">
      <FormControl>
        <InputLabel>Year</InputLabel>
        <Select
          label="Year"
          value={selectedYear}
          onChange={(e) => setSelectedYear(e.target.value)}
          required
        >
          {yearsList.map((year) => (
            <MenuItem key={year} value={year}>
              {year}
            </MenuItem>
          ))}
        </Select>
      </FormControl>
      <Bar options={options} data={chartData} />
    </div>
  );
}

export default TransactionsChart;
