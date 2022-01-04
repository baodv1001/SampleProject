import { EyeOutlined, EditOutlined, HeartOutlined, DeleteOutlined } from '@ant-design/icons';
import { Table, Card, Row, Col, Input, Modal, Tooltip, Button } from 'antd';
import moment from 'moment';
import { useNavigate } from 'react-router-dom';
import React, { useEffect, useState } from 'react';
import InfiniteScroll from 'react-infinite-scroll-component';
import { useDispatch, useSelector } from 'react-redux';
import userApi from '../../api/userApi';
import { getUser } from '../../redux/actions/auth';
import { getEmployees } from '../../redux/actions/employees';
import { authState$, employeeState$ } from '../../redux/selectors';

const { Search } = Input;
const { confirm } = Modal;
const Employee = props => {
  const dispatch = useDispatch();
  let navigate = useNavigate();
  const [dataSource, setDataSource] = useState([]);
  const { data, isLoading, isSuccess } = useSelector(employeeState$);
  const columns = [
    {
      title: 'No',
      dataIndex: 'no',
    },
    {
      title: 'Name',
      dataIndex: 'name',
    },
    {
      title: 'Address',
      dataIndex: 'address',
    },
    {
      title: 'Level',
      dataIndex: 'level',
    },
    {
      title: 'DOB',
      dataIndex: 'dob',
      width: "20%"
    },
    {
      title: '',
      dataIndex: 'action',
      width: '10%',
      render: idEmployee => (
        <div className={role === 'admin' && 'flex'}>
          <Tooltip title="View details">
            <Button  icon={<EditOutlined />} onClick={() => navigate(`/employee/${idEmployee}`)} />
          </Tooltip>
          {role === 'admin' && (
            <Tooltip title="Delete">
              <Button danger icon={<DeleteOutlined />} onClick={() => handleDelete(idEmployee)} />
            </Tooltip>
          )}
        </div>
      ),
    }
  ];
  const [role, setRole] = useState();

  const handleDelete = idEmployee => {};

  useEffect(() => {
    const role = localStorage.getItem('role');
    setRole(role);
    dispatch(getEmployees.getEmployeesRequest());
  }, []);
  useEffect(() => {
    console.log(data);
    if(data.length>0)
    {
      mappingDatasource(data);
    }
  }, [data]);
  const handleSearch = value => {
    const dataTmp = data.filter(
      item => item.courseName.toLowerCase().search(value.toLowerCase()) >= 0
    );
    mappingDatasource(dataTmp);
  };
  const mappingDatasource = dataInput => {
    const res = [];
    dataInput.map(employee => {
      // const { Role } = employee.Role;
      var no=0;
      res.push({
        no: ++no,
        idEmployee: employee.idEmployee,
        name: employee.name,
        level: employee.level,
        dob: moment(employee.dob).format("DD/MM/YYYY"),
        address: employee.address,
        // role: Role.name,
      });
    });
    setDataSource(res);
  };
  return (
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
            }}
          />
        </Col>
      </Row>
    </Card>
  );
};

export default Employee;
