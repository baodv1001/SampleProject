import { LockTwoTone, UserOutlined } from '@ant-design/icons';
import { Alert, Button, Card, Col, Form, Input, Row, notification } from 'antd';
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import styles from './index.module.less';
import authApi from 'api/authApi';
const Register = props => {
  const [loading, setLoading] = useState(false);
  const [isFailed, setIsFailed] = useState('0');
  const [failedMessage, setFailedMessage] = useState('');
  const [form] = Form.useForm();
  const navigate = useNavigate();
  const register = values => {
    setLoading(true);
    if (values.password !== values.confirmPassword) {
      notification.error({
        message: 'Error',
        description: 'Confirm Password must be same with password',
      });
      setLoading(false);

      return;
    }
    let data = {
      email: values.email,
      password: values.password,
    };
    authApi
      .resiger(data)
      .then(res => {
        notification.success({
          message: 'Success',
          description: 'Register successfully. Please Login',
        });
      })
      .catch(error => {
        notification.error({
          message: 'Error',
          description: 'Email is existed',
        });
      });
    setLoading(false);
  };
  let noticeFailed = () => {
    handleFailed('Please fill in all input fields!');
  };
  const handleFailed = message => {
    setFailedMessage(message);

    setIsFailed('1');
    setTimeout(function () {
      setIsFailed('0');
    }, 5000);
  };
  return (
    <div className={styles.content}>
      <div className={styles.container}>
        <Row justify="center">
          <Col xs={20} sm={20} md={12} lg={12}>
            <Card>
              <div style={{ margin: '1.5rem 0' }}>
                <div style={{ textAlign: 'center' }}>
                  <h3>Welcom Simple vote app!</h3>
                </div>
                <Row justify="center">
                  <Col xs={24} sm={24} md={20} lg={20}>
                    <div style={{ opacity: `${isFailed}` }}>
                      <Alert message={failedMessage} type="error" showIcon />
                    </div>
                    <Form
                      id="login-form"
                      layout="vertical"
                      onFinish={register}
                      onFinishFailed={noticeFailed}
                      form={form}
                      initialValues={{
                        remember: true,
                      }}>
                      <Form.Item
                        label="Email"
                        name="email"
                        rules={[
                          {
                            required: true,
                            message: 'Invalid type of email!',
                            type: 'email',
                          },
                        ]}>
                        <Input
                          placeholder="Email"
                          prefix={<UserOutlined style={{ color: '#3e79f7' }} />}></Input>
                      </Form.Item>
                      <Form.Item
                        label="Password"
                        name="password"
                        rules={[
                          {
                            required: true,
                            message: 'Please input your password!',
                          },
                          {
                            min: 6,
                            message: 'Password must be minimum 6 characters.',
                          },
                        ]}>
                        <Input.Password
                          placeholder="Password"
                          prefix={<LockTwoTone />}></Input.Password>
                      </Form.Item>
                      <Form.Item
                        label="Confirm Password"
                        name="confirmPassword"
                        rules={[
                          {
                            required: true,
                            message: 'Please input confirm password!',
                          },
                          {
                            min: 6,
                            message: 'Password must be minimum 6 characters.',
                          },
                        ]}>
                        <Input.Password
                          placeholder="Confirm Password"
                          prefix={<LockTwoTone />}></Input.Password>
                      </Form.Item>
                      <Form.Item>
                        <Button
                          loading={loading}
                          className={styles['btn-login']}
                          type="link"
                          onClick={() => {
                            navigate('/login');
                          }}>
                          Login
                        </Button>
                      </Form.Item>
                      <Form.Item>
                        <Button
                          size="large"
                          type="primary"
                          block="true"
                          htmlType="submit"
                          loading={loading}>
                          Register
                        </Button>
                      </Form.Item>
                    </Form>
                  </Col>
                </Row>
              </div>
            </Card>
          </Col>
        </Row>
      </div>
    </div>
  );
};

export default Register;
