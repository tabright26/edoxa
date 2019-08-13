import React, { Component } from "react";
import { FormGroup, Col, Label, Input, Form, Button } from "reactstrap";

class AddressBookForm extends Component {
  render() {
    return (
      <Form>
        <FormGroup row>
          <Col md="2">
            <Label htmlFor="street">Street</Label>
          </Col>
          <Col xs="12" md="10">
            <Input type="text" id="street" placeholder="Enter street name" bsSize="sm" />
          </Col>
        </FormGroup>
        <FormGroup row className="my-0">
          <Col xs="8">
            <FormGroup row>
              <Col md="3">
                <Label htmlFor="city">City</Label>
              </Col>
              <Col xs="12" md="9">
                <Input type="text" id="city" placeholder="Enter your city" bsSize="sm" />
              </Col>
            </FormGroup>
          </Col>
          <Col xs="4">
            <FormGroup row>
              <Col md="6">
                <Label htmlFor="postal-code">Postal Code</Label>
              </Col>
              <Col xs="12" md="6">
                <Input type="text" id="postal-code" placeholder="Postal Code" bsSize="sm" />
              </Col>
            </FormGroup>
          </Col>
        </FormGroup>
        <FormGroup row>
          <Col md="2">
            <Label htmlFor="country">Country</Label>
          </Col>
          <Col xs="12" md="10">
            <Input type="text" id="country" placeholder="Country name" bsSize="sm" />
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

export default AddressBookForm;
