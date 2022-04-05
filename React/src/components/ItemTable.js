import * as React from 'react';
import { DataGrid } from '@mui/x-data-grid';

const columns = [
    { field: 'name', headerName: 'Task', width: 400 },
    { field: 'isComplete', headerName: 'Completed', width: 400 },
];

export default function ItemTable({list}) {
    return (
        <div style={{ marginTop:'10px',margin:'auto',height: 300, width: '40%' }}>
            <DataGrid
                rows={list}
                columns={columns}
                pageSize={5}
                rowsPerPageOptions={[5]}
                checkboxSelection
            />
        </div>
    );
}