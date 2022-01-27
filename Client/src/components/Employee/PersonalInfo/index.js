import { Button, Card, Col, DatePicker, Form, Input, notification, Row, Select } from 'antd';
import moment from 'moment';
import React, { useState, useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { useParams } from 'react-router';
import * as employeeActions from 'redux/actions/employees';
import { employeeState$ } from 'redux/selectors';
import { dateValidator, phoneNumberValidator } from 'utils/validator';
import PropTypes from 'prop-types';

const { Option } = Select;

const PersonalInfo = props => {
  let navigate = useNavigate();
  const [isSubmit, setIsSubmit] = useState(false);
  const dispatch = useDispatch();
  const { data: employees, isLoading, isSuccess, message } = useSelector(employeeState$);
  const [form] = Form.useForm();
  const { id } = useParams();
  const dateFormat = 'DD/MM/YYYY';
  const { typeSubmit } = props;
  const role = localStorage.getItem('role');

  const handleSubmit = () => {
    const data = form.getFieldValue();
    const { name, gender, dob, phonenumber, email, address, level } = data;
    data.imageUrl = props.imgUrl;
    const currentDate = moment();

    if (currentDate < dob) {
      notification.error({ message: 'Date of birth is not greater than current date' });
    } else {

      // create employee
      if (name && gender && dob && phonenumber && email && address && level) {
        if (typeSubmit === 'Create') {
          data.updatedAt = moment();
          data.createdAt = moment();
          dispatch(employeeActions.createEmployee.createEmployeeRequest(data));
          setIsSubmit(true);
        }
      }

      // edit employee
      if (typeSubmit === 'Edit') {
        var editedEmployee = employees.find(employee => employee.id == id);
        editedEmployee = { ...editedEmployee, ...data };
        dispatch(employeeActions.updateEmployee.updateEmployeeRequest(editedEmployee));
        setIsSubmit(true);
      }
    }
  };

  // Fill form
  useEffect(() => {
    if (id && employees.length !== 0) {
      const employee = employees.find(employee => employee.id == id);
      const editedLecturer = {
        name: employee.name,
        gender: employee.gender,
        level: employee.level,
        dob: moment(employee.dob),
        phonenumber: employee.phonenumber,
        email: employee.email,
        address: employee.address,
      };

      props.setImgUrl(employee?.imageUrl);
      form.setFieldsValue(editedLecturer);
    }
  }, [employees]);

  // Get all employees
  useEffect(() => {
    dispatch(employeeActions.getEmployees.getEmployeesRequest());
  }, [dispatch]);
  // Handle noti when get respone

  useEffect(() => {
    if (!isLoading && isSubmit) {
      if (isSuccess) {
        notification.success({ message: message });
        navigate('/employee');
      } else {
        notification.error({ message: message });
      }
    }
  }, [isSuccess, isLoading]);

  return (
    <Card>
      <Form form={form} layout="vertical">
        <Row gutter={20} align="center">
          <Col xs={24} md={24} xl={10} lg={10}>
            <Form.Item label="Full name" name="name" rules={[{ required: true }]}>
              <Input disabled={role !== 'Admin'} placeholder="Full name" />
            </Form.Item>
          </Col>
          <Col xs={24} md={24} xl={10} lg={10}>
            <Form.Item
              label="DOB"
              name="dob"
              rules={[
                { required: true },
                {
                  validator: dateValidator,
                },
              ]}>
              <DatePicker
                disabled={role !== 'Admin'}
                format={dateFormat}
                style={{ width: '100%' }}
              />
            </Form.Item>
          </Col>
          <Col xs={24} md={24} xl={20} lg={20}>
            <Form.Item label="Address" name="address" rules={[{ required: true }]}>
              <Input disabled={role !== 'Admin'} placeholder="Address" />
            </Form.Item>
          </Col>
          <Col xs={24} md={24} xl={10} lg={10}>
            <Form.Item label="Gender" name="gender" rules={[{ required: true }]}>
              <Select disabled={role !== 'Admin'}>
                <Option value="Male">Male</Option>
                <Option value="Female">Female</Option>
                <Option value="Others">Others</Option>
              </Select>
            </Form.Item>
          </Col>
          <Col xs={24} md={24} xl={10} lg={10}>
            <Form.Item label="Level" name="level" rules={[{ required: true }]}>
              <Select disabled={role !== 'Admin'}>
                <Option value="Intern">Intern</Option>
                <Option value="Senior Dev">Senior Dev</Option>
                <Option value="Tech lead">Tech lead</Option>
              </Select>
            </Form.Item>
          </Col>
          <Col xs={24} md={24} lg={10} xl={10}>
            <Form.Item
              label="Phone number"
              name="phonenumber"
              onKeyPress={event => {
                if (!/[0-9]/.test(event.key)) {
                  event.preventDefault();
                }
              }}
              rules={[{ required: true }]}>
              <Input
                disabled={role !== 'Admin'}
                type="text"
                placeholder="Phone number"
                minLength={10}
                maxLength={10}
              />
            </Form.Item>
          </Col>
          <Col xs={24} md={24} lg={10} xl={10}>
            <Form.Item label="Email" name="email" rules={[{ required: true, type: 'email' }]}>
              <Input disabled={role !== 'Admin'} placeholder="Email" />
            </Form.Item>
          </Col>
        </Row>
        <Row align="center">
          <Form.Item>
            <Button
              disabled={role !== 'Admin'}
              onClick={handleSubmit}
              loading={isLoading}
              type="primary"
              htmlType="submit">
              Submit
            </Button>
          </Form.Item>
        </Row>
      </Form>
    </Card>
  );
};

PersonalInfo.propsType = {
  typeSubmit: PropTypes.string,
  imgUrl: PropTypes.string,
  setImgUrl: PropTypes.func
}

export default PersonalInfo;
