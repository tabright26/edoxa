import React, { Component } from "react";
import { TabPane, Card, CardHeader, CardBody } from "reactstrap";

import PhoneNumberData from "./Data";

class PhoneNumberTabPanel extends Component {
  render() {
    const { tabId } = this.props;
    return (
      <TabPane tabId={tabId} className="p-0 my-4">
        <Card className="card-accent-primary m-0 p-0">
          <CardHeader>
            <strong>PHONE NUMBER</strong>
            <div className="card-header-actions">
              <i className="fa fa-edit float-right" />
            </div>
          </CardHeader>
          <CardBody>
            <PhoneNumberData phoneNumber="1231231231" />
          </CardBody>
        </Card>
      </TabPane>
    );
  }
}

export default PhoneNumberTabPanel;
