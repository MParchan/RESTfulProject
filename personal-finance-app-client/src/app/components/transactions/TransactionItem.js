import { KeyboardArrowDown, KeyboardArrowUp } from "@mui/icons-material";
import {
  Box,
  Collapse,
  IconButton,
  TableCell,
  TableRow,
  Typography,
} from "@mui/material";
import { format } from "date-fns";
import React, { useState } from "react";

function TransactionItem(props) {
  const [open, setOpen] = useState(false);

  return (
    <React.Fragment>
      <TableRow
        key={props.transaction.id}
        sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
      >
        <TableCell className={open ? "border border-white" : ""}>
          <IconButton
            aria-label="expand row"
            size="small"
            onClick={() => setOpen(!open)}
          >
            {open ? <KeyboardArrowUp /> : <KeyboardArrowDown />}
          </IconButton>
        </TableCell>

        <TableCell className={open ? "border border-white" : ""} align="center">
          {format(new Date(props.transaction.date), "dd.MM.yyyy HH:mm")}
        </TableCell>
        <TableCell className={open ? "border border-white" : ""} align="center">
          {props.transaction.category}
        </TableCell>
        <TableCell
          className={open ? "border border-white" : ""}
          align="center"
          component="th"
          scope="row"
        >
          {props.transaction.type === "income" ? (
            <b className="text-success">
              + ${props.transaction.amount.toFixed(2)}
            </b>
          ) : (
            <b className="text-danger">
              - ${props.transaction.amount.toFixed(2)}
            </b>
          )}
        </TableCell>
      </TableRow>
      <TableRow>
        <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={4}>
          <Collapse in={open} timeout="auto" unmountOnExit>
            <Box sx={{ margin: 1 }}>
              <Typography gutterBottom component="p">
                <b>Comment:</b> {props.transaction.comment}
              </Typography>
            </Box>
          </Collapse>
        </TableCell>
      </TableRow>
    </React.Fragment>
  );
}
export default TransactionItem;
