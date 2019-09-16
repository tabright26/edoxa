import React from "react";
import { Col, FormGroup, Row } from "reactstrap";
import { Form, Field, reduxForm } from "redux-form";
import Button from "components/Shared/Override/Button";
import Input from "components/Shared/Override/Input";
import { UPDATE_CREDITCARD_FORM } from "forms";
import validate from "./validate";

const UpdateStripeCreditCardForm = ({ handleSubmit, handleCancel }) => (
  <Form onSubmit={handleSubmit}>
    <Row>
      <Col xs="4">
        <FormGroup>
          <label>Exp.Month</label>
          <Field type="select" name="exp_month" component={Input.Select}>
            <option value="1">1</option>
            <option value="2">2</option>
            <option value="3">3</option>
            <option value="4">4</option>
            <option value="5">5</option>
            <option value="6">6</option>
            <option value="7">7</option>
            <option value="8">8</option>
            <option value="9">9</option>
            <option value="10">10</option>
            <option value="11">11</option>
            <option value="12">12</option>
          </Field>
        </FormGroup>
      </Col>
      <Col xs="8">
        <FormGroup>
          <label>Exp.Year</label>
          <Field type="select" name="exp_year" component={Input.Select}>
            <option>2017</option>
            <option>2018</option>
            <option>2019</option>
            <option>2020</option>
            <option>2021</option>
            <option>2022</option>
            <option>2023</option>
            <option>2024</option>
            <option>2025</option>
            <option>2026</option>
          </Field>
        </FormGroup>
      </Col>
    </Row>
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={handleCancel} />
    </FormGroup>
  </Form>
);

export default reduxForm({ form: UPDATE_CREDITCARD_FORM, validate })(UpdateStripeCreditCardForm);
