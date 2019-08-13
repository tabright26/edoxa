import React, { Component } from "react";
import { FormGroup, Col, Label, Input, Form, Button } from "reactstrap";

class PersonalInfoForm extends Component {
  render() {
    return (
      <Form>
        <FormGroup row className="my-0">
          <Col xs="6">
            <FormGroup row>
              <Col md="4">
                <Label htmlFor="city">First Name</Label>
              </Col>
              <Col xs="12" md="8">
                <Input type="text" id="firstName" bsSize="sm" placeholder="Enter your first name" />
              </Col>
            </FormGroup>
          </Col>
          <Col xs="6">
            <FormGroup row>
              <Col md="4">
                <Label htmlFor="postal-code">Last Name</Label>
              </Col>
              <Col xs="12" md="8">
                <Input type="text" id="lastName" bsSize="sm" placeholder="Enter your last name" />
              </Col>
            </FormGroup>
          </Col>
        </FormGroup>
        <FormGroup row>
          <Col md="2">
            <Label htmlFor="birthDate">Birth date</Label>
          </Col>
          <Col md="10">
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
          </Col>
        </FormGroup>
        <FormGroup row>
          <Col md="2">
            <Label htmlFor="gender">Gender</Label>
          </Col>
          <Col xs="12" md="2">
            <Input type="select" name="gender" id="gender" bsSize="sm">
              <option value="0">Please select</option>
              <option value="Male">Male</option>
              <option value="Female">Female</option>
              <option value="Other">Other</option>
            </Input>
          </Col>
        </FormGroup>
        <FormGroup className="mb-0">
          <Button type="submit" size="sm" color="primary" className="mr-2">
            Save Changes
          </Button>
          <Button type="reset" size="sm" color="secondary" outline>
            Cancel
          </Button>
        </FormGroup>
      </Form>
    );
  }
}

export default PersonalInfoForm;
