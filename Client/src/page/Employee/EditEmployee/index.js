import { Breadcrumb, Card, Col, Row } from 'antd';
import ImageUploader from 'components/common/ImageUploader';
import PersonalInfo from 'components/Employee/PersonalInfo/index';
import React, { useState } from 'react';
import { useParams } from 'react-router';

export default function EditEmployee() {
  const { id } = useParams();
  const [imgUrl, setImgUrl] = useState('');

  return (
    <div>
      <Breadcrumb style={{ marginBottom: '20px' }}>
        <Breadcrumb.Item href="/employee">Employees</Breadcrumb.Item>
        <Breadcrumb.Item>{!id ? 'Add Employee' : 'Edit Employee'}</Breadcrumb.Item>
      </Breadcrumb>
      {id ? <h3>Edit employee</h3> : <h3>Add new employee</h3>}
      <Row gutter={[20, 20]}>
        <Col span={18}>
          {id ? (
            <PersonalInfo imgUrl={imgUrl} setImgUrl={setImgUrl} typeSubmit="Edit" />
          ) : (
            <PersonalInfo imgUrl={imgUrl} setImgUrl={setImgUrl} typeSubmit="Create" />
          )}
        </Col>
        <Col span={6}>
          <Card>
            <h4>Avatar</h4>
            <ImageUploader onUploaded={setImgUrl} url={imgUrl} />
          </Card>
        </Col>
      </Row>
    </div>
  );
}
