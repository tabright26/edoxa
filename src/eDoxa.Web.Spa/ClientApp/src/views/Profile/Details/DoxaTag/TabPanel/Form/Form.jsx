import React, { Component } from "react";
import { FormGroup, Input, Form, Button, Col } from "reactstrap";

class DoxaTagForm extends Component {
  render() {
    return (
      <Form>
        <FormGroup row>
          <Col xs="12" md="6">
            <Input type="text" id="doxaTag" bsSize="sm" placeholder="Enter your Doxatag" />
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

export default DoxaTagForm;
