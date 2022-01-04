const employeeState$ = state => state.employees;
const authState$ = state => state.auth.data;

export { employeeState$, authState$ };
