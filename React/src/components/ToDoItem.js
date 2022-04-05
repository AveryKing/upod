import React, {useState} from 'react';
import {Checkbox, Typography} from "@mui/material";
import TableCell from "@mui/material/TableCell";
import TableRow from "@mui/material/TableRow";

const ToDoItem = ({item}) => {
    const [checked, setChecked] = useState(false);

    const handleChange = () => {

    }

    return (
        <TableRow
            key={item.name}
            sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
        >
            <TableCell component="th" scope="row">
                {item.name}
            </TableCell>
            <TableCell align="right">{item.isComplete}</TableCell>
            {/** <TableCell align="right">{row.fat}</TableCell>
            <TableCell align="right">{row.carbs}</TableCell> **/}

        </TableRow>
    );
};

export default ToDoItem;