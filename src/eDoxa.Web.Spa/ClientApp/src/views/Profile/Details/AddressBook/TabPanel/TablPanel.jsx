import React, { Component } from "react";
import { connect } from "react-redux";
import { show } from "redux-modal";
import { TabPane, Card, CardHeader, CardBody } from "reactstrap";

import AddressBookData from "./Data";

import AddressBookModal from "../Modal/Modal";

class AddressBookTabPanel extends Component {
  handleOpen = name => () => {
    this.props.actions.show(name);
  };

  render() {
    const { tabId } = this.props;
    return (
      <>
        <TabPane tabId={tabId} className="p-0 my-4">
          <Card className="card-accent-primary m-0 p-0">
            <CardHeader>
              <strong>ADDRESS BOOK</strong>
              <div className="card-header-actions" onClick={this.handleOpen("myModal")}>
                <i className="fa fa-edit float-right" />
              </div>
            </CardHeader>
            <CardBody>
              <AddressBookData />
            </CardBody>
          </Card>
        </TabPane>
      </>
    );
  }
}

const mapDispatchToProps = dispatch => {
  return {
    actions: {
      show: name => dispatch(show(name))
    }
  };
};

export default connect(
  null,
  mapDispatchToProps
)(AddressBookTabPanel);
