import React, { Component } from "react";
import { TabPane, Card, CardHeader, CardBody } from "reactstrap";

//import PersonalInfoForm from "./Form";
import PersonalInfoData from "./Data";

class PersonelInformationTabPanel extends Component {
  render() {
    const { tabId } = this.props;
    return (
      <TabPane tabId={tabId} className="p-0 my-4">
        <Card className="card-accent-primary m-0 p-0">
          <CardHeader>
            <strong>PERSONAL INFORMATIONS</strong>
            <div className="card-header-actions">
              <i className="fa fa-edit float-right" />
            </div>
          </CardHeader>
          <CardBody>
            <PersonalInfoData />
          </CardBody>
        </Card>
      </TabPane>
    );
  }
}

export default PersonelInformationTabPanel;
