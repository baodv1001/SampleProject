import { LockTwoTone, UserOutlined } from '@ant-design/icons';
import { Alert, Button, Card, Col, Form, Input, Row } from 'antd';
import React, { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { getAuth } from '../../redux/actions/auth';
import { authState$ } from '../../redux/selectors';
import styles from './index.module.less';

const Login = () => {
  const [loading, setLoading] = useState(false);
  const [isFailed, setIsFailed] = useState('0');
  const [failedMessage, setFailedMessage] = useState('');
  const [form] = Form.useForm();
  const dispatch = useDispatch();
  const auth = useSelector(authState$);
  const navigate = useNavigate();

  const login = values => {
    setLoading(true);
    let data = {
      username: values.username,
      password: values.password,
    };
    dispatch(getAuth.getAuthRequest(data));
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

  // Check respone when login
  useEffect(() => {
    if (auth?.data?.jwt) {
          // Set token to localstorage
          localStorage.setItem('accessToken', auth.data.jwt);
          localStorage.setItem('idUser', auth.data.user.idUser);
          localStorage.setItem('role', auth.data.user.role.name);

          navigate('/', {replace:true});
          window.location.reload();
        } else {
          setLoading(false);
          handleFailed(auth.data.message);
        }
    }, [auth, navigate]);

  return (
    <div className={styles.content}>
      <div className={styles.container}>
        <Row justify="center">
          <Col xs={20} sm={20} md={12} lg={12}>
            <Card>
              <div style={{ margin: '1.5rem 0' }}>
                <div style={{ textAlign: 'center' }}>
                  <h3>Welcome to Sample app!</h3>
                </div>
                <Row justify="center">
                  <Col xs={24} sm={24} md={20} lg={20}>
                    <div style={{ opacity: `${isFailed}` }}>
                      <Alert message={failedMessage} type="error" showIcon />
                    </div>
                    <Form
                      id="login-form"
                      layout="vertical"
                      onFinish={login}
                      onFinishFailed={noticeFailed}
                      form={form}
                      initialValues={{
                        remember: true,
                      }}>
                      <Form.Item
                        label="Username"
                        name="username"
                        rules={[
                          {
                            required: true,
                          },
                        ]}>
                        <Input
                          placeholder="Username"
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
                            min: 5,
                            message: 'Password must be minimum 6 characters.',
                          },
                        ]}>
                        <Input.Password
                          placeholder="Password"
                          prefix={<LockTwoTone />}></Input.Password>
                      </Form.Item>
                      <Form.Item>
                        <Button
                          className={styles['btn-register']}
                          type="link"
                          onClick={() => {
                            navigate('/register');
                          }}>
                          Register
                        </Button>
                      </Form.Item>
                      <Form.Item>
                        <Button
                          size="large"
                          type="primary"
                          block="true"
                          htmlType="submit"
                          loading={loading}>
                          Sign In
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

export default Login;
