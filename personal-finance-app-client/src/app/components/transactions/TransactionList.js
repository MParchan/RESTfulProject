import {
  CircularProgress,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableFooter,
  TablePagination,
  TableRow,
} from "@mui/material";
import { format } from "date-fns";
import { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import TablePaginationActions from "../../ui/table/TablePaginationActions";
import { getComparator, stableSort } from "../../ui/table/TableSortActions";
import TransactionTableHead from "../../ui/table/TransactionTableHead";
import { getTransactions } from "./getTransactions";
import TransactionItem from "./TransactionItem";

function TransactionList(props) {
  const { userEmail } = useSelector((state) => state.auth);
  const [loadedTransactions, setLoadedTransactions] = useState([]);
  const [listOfTransactions, setListOFTransactions] = useState([]);
  const [loading, setLoading] = useState(true);
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(10);
  const [order, setOrder] = useState("desc");
  const [orderBy, setOrderBy] = useState("date");

  useEffect(() => {
    const params = {
      email: userEmail,
      setLoading: setLoading,
      setLoadedTransactions: setLoadedTransactions,
    };
    getTransactions(params);
  }, [userEmail]);

  useEffect(() => {
    var min = 0;
    var max = 0;
    for (var key in loadedTransactions) {
      if (key === "0") {
        min = loadedTransactions[key].amount;
        max = loadedTransactions[key].amount;
      }
      if (min > loadedTransactions[key].amount) {
        min = loadedTransactions[key].amount;
      }
      if (max < loadedTransactions[key].amount) {
        max = loadedTransactions[key].amount;
      }
    }
    props.setMin(min);
    props.setMax(max);
  }, [loadedTransactions, props]);
  useEffect(() => {
    let transactions = [];
    if (props.startDate !== null) {
      const startDate = format(new Date(props.startDate), "MM/dd/yyyy");
      for (var key in loadedTransactions) {
        let transactionDate = format(
          new Date(loadedTransactions[key].date),
          "MM/dd/yyyy"
        );
        if (compareDates(startDate, transactionDate)) {
          transactions.push(loadedTransactions[key]);
        }
      }
    } else {
      transactions = loadedTransactions;
    }
    if (props.type === "income" || props.type === "expenditure") {
      transactions = transactions.filter((t) => t.type === props.type);
    }
    transactions = transactions.filter((t) => t.amount >= props.amount[0]);
    transactions = transactions.filter((t) => t.amount <= props.amount[1]);
    setListOFTransactions(transactions);
    setPage(0);
  }, [loadedTransactions, props.amount, props.startDate, props.type]);

  useEffect(() => {
    var inc = 0;
    var exp = 0;
    for (var key in listOfTransactions) {
      if (listOfTransactions[key].type === "income") {
        inc += listOfTransactions[key].amount;
      }
      if (listOfTransactions[key].type === "expenditure") {
        exp += listOfTransactions[key].amount;
      }
    }
    props.setIncome(inc.toFixed(2));
    props.setExpenditure(exp.toFixed(2));
  }, [listOfTransactions, props]);

  const compareDates = (d1, d2) => {
    let date1 = new Date(d1).getTime();
    let date2 = new Date(d2).getTime();
    if (date1 > date2) {
      return false;
    } else {
      return true;
    }
  };

  const emptyRows =
    page > 0
      ? Math.max(0, (1 + page) * rowsPerPage - listOfTransactions.length)
      : 0;

  const handleChangePage = (event, newPage) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  const handleRequestSort = (event, property) => {
    const isAsc = orderBy === property && order === "asc";
    setOrder(isAsc ? "desc" : "asc");
    setOrderBy(property);
  };

  if (loading) {
    return (
      <div className="text-center">
        <CircularProgress color="inherit" />
      </div>
    );
  }
  return (
    <Paper>
      <TableContainer>
        <Table sx={{ minWidth: 500 }}>
          <TransactionTableHead
            order={order}
            orderBy={orderBy}
            onRequestSort={handleRequestSort}
            rowCount={listOfTransactions.length}
          />
          <TableBody>
            {stableSort(listOfTransactions, getComparator(order, orderBy))
              .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
              .map((transaction) => (
                <TransactionItem
                  key={transaction.id}
                  transaction={transaction}
                />
              ))}
            {emptyRows > 0 && (
              <TableRow style={{ height: 53 * emptyRows }}>
                <TableCell colSpan={4} />
              </TableRow>
            )}
          </TableBody>
          <TableFooter>
            <TableRow>
              <TablePagination
                rowsPerPageOptions={[5, 10, 25, { label: "All", value: -1 }]}
                count={listOfTransactions.length}
                rowsPerPage={rowsPerPage}
                page={page}
                onPageChange={handleChangePage}
                onRowsPerPageChange={handleChangeRowsPerPage}
                ActionsComponent={TablePaginationActions}
              />
            </TableRow>
          </TableFooter>
        </Table>
      </TableContainer>
    </Paper>
  );
}
export default TransactionList;
