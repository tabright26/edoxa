import React from "react";
import { reduxForm } from "redux-form";
import { FormGroup, Col, Input, Form } from "reactstrap";
import Button from "../../../../components/Button";
import { UPDATE_PERSONALINFO_FORM } from "../forms";

const UpdatePersonalInfoForm = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <FormGroup row className="my-0">
      <Col xs="6">
        <FormGroup>
          <Input type="text" id="firstName" bsSize="sm" placeholder="Enter your first name" />
        </FormGroup>
      </Col>
      <Col xs="6">
        <FormGroup>
          <Input type="text" id="lastName" bsSize="sm" placeholder="Enter your last name" />
        </FormGroup>
      </Col>
    </FormGroup>
    <FormGroup>
      <Input className="d-inline" type="select" name="months" id="month" bsSize="sm" style={{ width: "60px" }}>
        <option value="0">mm</option>
        <option value="Male">Male</option>
        <option value="Female">Female</option>
        <option value="Other">Other</option>
      </Input>
      <span className="d-inline">/</span>
      <Input className="d-inline" type="select" name="days" id="day" bsSize="sm" style={{ width: "60px" }}>
        <option value="0">dd</option>
        <option value="Male">Male</option>
        <option value="Female">Female</option>
        <option value="Other">Other</option>
      </Input>
      <span className="d-inline">/</span>
      <Input className="d-inline" type="select" name="years" id="year" bsSize="sm" style={{ width: "75px" }}>
        <option value="0">YYYY</option>
        <option value="Male">Male</option>
        <option value="Female">Female</option>
        <option value="Other">Other</option>
      </Input>
    </FormGroup>
    <FormGroup>
      <Input type="select" name="gender" id="gender" bsSize="sm">
        <option value="0">Please select</option>
        <option value="Male">Male</option>
        <option value="Female">Female</option>
        <option value="Other">Other</option>
      </Input>
    </FormGroup>
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: UPDATE_PERSONALINFO_FORM })(UpdatePersonalInfoForm);
