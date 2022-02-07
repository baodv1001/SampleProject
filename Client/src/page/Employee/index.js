import { DeleteOutlined, EditOutlined, ExclamationCircleOutlined } from '@ant-design/icons';
import {
  Button,
  Card,
  Col,
  Input,
  Modal,
  notification,
  Row,
  Table,
  Tooltip,
  Breadcrumb,
} from 'antd';
import employeeApi from 'api/employeeApi';
import moment from 'moment';
import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { deleteEmployee, getEmployees } from '../../redux/actions/employees';
import { employeeState$ } from '../../redux/selectors';

const { Search } = Input;
const { confirm } = Modal;

const Employee = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [dataSource, setDataSource] = useState([]);
  const { data, isLoading, isSuccess } = useSelector(employeeState$);
  const [role, setRole] = useState();
  let page=1, pageSize=10;

  const columns = [
    {
      title: 'No',
      dataIndex: 'no',
      ellipsis: true,
    },
    {
      title: 'Name',
      dataIndex: 'name',
      ellipsis: true,
    },
    {
      title: 'Gender',
      dataIndex: 'gender',
      align: 'center',
      filters: [
        { text: 'Male', value: 'Male' },
        { text: 'Female', value: 'Female' },
        { text: 'Others', value: 'Others' },
      ],
      filterSearch: true,
      onFilter: (value, record) => record.gender.startsWith(value),
      ellipsis: true,
    },
    {
      title: 'Address',
      dataIndex: 'address',
      ellipsis: true,
    },
    {
      title: 'Level',
      dataIndex: 'level',
      align: 'center',
      filters: [
        {
          text: 'Intern',
          value: 'Intern',
        },
        {
          text: 'Senior Dev',
          value: 'Senior Dev',
        },
        {
          text: 'Tech lead',
          value: 'Tech lead',
        },
      ],
      filterSearch: true,
      onFilter: (value, record) => record.level.startsWith(value),
      ellipsis: true,
    },
    {
      title: 'DOB',
      dataIndex: 'dob',
      width: '20%',
      align: 'center',
      sorter: (a, b) => moment(a.dob, 'DD/MM/YYYY').unix() - moment(b.dob, 'DD/MM/YYYY').unix(),
    },
    {
      title: '',
      dataIndex: 'idEmployee',
      width: '10%',
      align: 'center',
      render: idEmployee => (
        <div className={role === 'Admin' && 'flex'}>
          <Tooltip title="View details/Edit">
            <Button
              type="primary"
              icon={<EditOutlined />}
              onClick={() => navigate(`/employee/edit/${idEmployee}`)}
            />
          </Tooltip>
          {role === 'Admin' && (
            <Tooltip title="Delete">
              <Button danger icon={<DeleteOutlined />} onClick={() => handleDelete(idEmployee)} />
            </Tooltip>
          )}
        </div>
      ),
    },
  ];

  const handleDelete = idEmployee => {
    confirm({
      title: 'Do you want to delete this employee?',
      icon: <ExclamationCircleOutlined />,
      content: '',
      onOk() {
        //dispatch(deleteEmployee.deleteEmployeeRequest(idEmployee));
        employeeApi.delete(idEmployee)
        notification.success({ message: 'Delete Employee successful!' });
      },
      onCancel() {},
    });
  };

  const handleSearch = value => {
    const dataTmp = data.filter(
      employee => employee.name.toLowerCase().search(value.toLowerCase()) >= 0
    );
    mappingDatasource(dataTmp);
  };

  // get role and get all employees
  useEffect(() => {
    const role = localStorage.getItem('role');
    setRole(role);
    employeeApi.getAll(page,pageSize).then(res=>mappingDatasource(res))
  }, []);

  // Handle data from back end
  useEffect(() => {
    if (data.length > 0) {
      mappingDatasource(data);
    }
  }, [data]);
  
  // Map data and format 
  const mappingDatasource = dataInput => {
    const res = [];
    var no = 0;

    dataInput.map(employee => {
      res.push({
        no: ++no,
        idEmployee: employee.id,
        name: employee.name,
        gender: employee.gender,
        level: employee.level,
        dob: moment(employee.dob).format('DD/MM/YYYY'),
        address: employee.address,
        // role: Role.name,
      });
    });

    setDataSource(res);
  };

  const onChange = (page, pageSize) => {
    employeeApi.getAll(page,pageSize).then(res=>mappingDatasource(res))
  }

  return (
    <div>
      <Breadcrumb style={{ marginBottom: '20px' }}>
        <Breadcrumb.Item href="/employee">Employees</Breadcrumb.Item>
      </Breadcrumb>
      <h3>List Employee</h3>
      <Card>
        <Row gutter={[20, 20]}>
          <Col xs={24} sm={16} md={10} lg={8} xl={8}>
            <Search
              className="full"
              size="large"
              placeholder="Search by name"
              allowClear
              enterButton
              onSearch={handleSearch}
            />
          </Col>
          <Col flex="auto" />
          <Col span={6}>
            {role === 'admin' && (
              <Button size="large" type="primary" block onClick={() => navigate('/employee/add')}>
                Add Employee
              </Button>
            )}
          </Col>
          <Col span={24}>
            <Table
              bordered
              loading={isLoading}
              columns={columns}
              dataSource={dataSource}
              rowKey={row => row.idCourse}
              pagination={{
                defaultPageSize: 10,
                showSizeChanger: true,
                pageSizeOptions: ['10', '15', '20'],
                onChange: {onChange}
              }}
            />
          </Col>
        </Row>
      </Card>
    </div>
  );
};

export default Employee;
