import React, { Component } from "react";
import { TabPane, Card, CardHeader, CardBody } from "reactstrap";

//import DoxaTagForm from "./Form";
import DoxaTagData from "./Data";

class DoxaTagTabPanel extends Component {
  render() {
    const { tabId } = this.props;
    return (
      <TabPane tabId={tabId} className="p-0 my-4">
        <Card className="card-accent-primary m-0 p-0">
          <CardHeader>
            <strong>DOXATAG</strong>
            <div className="card-header-actions">
              <i className="fa fa-edit float-right" />
            </div>
          </CardHeader>
          <CardBody>
            <DoxaTagData />
          </CardBody>
        </Card>
      </TabPane>
    );
  }
}

export default DoxaTagTabPanel;
