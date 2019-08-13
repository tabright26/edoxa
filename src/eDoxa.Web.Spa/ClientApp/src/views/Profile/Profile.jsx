import React, { Component } from "react";
import { Col, Row, ListGroup, ListGroupItem, TabContent, TabPane } from "reactstrap";

import ProfileDetails from "./Details/Details";

class Profile extends Component {
  state = {
    activeTab: 0
  };

  toggle = activeTab => {
    this.setState({ activeTab });
  };

  render() {
    return (
      <Row>
        <Col xs="4" sm="3" md="2">
          <ListGroup id="list-tab-2" role="tablist" className="my-4">
            <ListGroupItem onClick={() => this.toggle(0)} action active={this.state.activeTab === 0}>
              Profile Overview
            </ListGroupItem>
            <ListGroupItem onClick={() => this.toggle(1)} action active={this.state.activeTab === 1}>
              Profile Details
            </ListGroupItem>
            <ListGroupItem onClick={() => this.toggle(2)} action active={this.state.activeTab === 2}>
              Security
            </ListGroupItem>
            <ListGroupItem onClick={() => this.toggle(3)} action active={this.state.activeTab === 3}>
              Confidentiality
            </ListGroupItem>
            <ListGroupItem onClick={() => this.toggle(4)} action active={this.state.activeTab === 4}>
              Connections
            </ListGroupItem>
            <ListGroupItem onClick={() => this.toggle(5)} action active={this.state.activeTab === 5}>
              Payment Methods
            </ListGroupItem>
            <ListGroupItem onClick={() => this.toggle(6)} action active={this.state.activeTab === 6}>
              Transaction History
            </ListGroupItem>
          </ListGroup>
        </Col>
        <Col xs="12" sm="12" md="7" lg="6">
          <TabContent activeTab={this.state.activeTab} className="border-0 my-4" style={{ background: "none" }}>
            <TabPane tabId={0} className="p-0">
              <h5 className="mb-4">PROFILE OVERVIEW</h5>
            </TabPane>
            <ProfileDetails tabId={1} />
            <TabPane tabId={2} className="p-0">
              <h5 className="mb-4">SECURITY</h5>
            </TabPane>
            <TabPane tabId={3} className="p-0">
              <h5 className="mb-4">CONFIDENTIALITY</h5>
            </TabPane>
            <TabPane tabId={4} className="p-0">
              <h5 className="mb-4">CONNECTIONS</h5>
            </TabPane>
            <TabPane tabId={5} className="p-0">
              <h5 className="mb-4">PAYMENT METHODS</h5>
            </TabPane>
            <TabPane tabId={6} className="p-0">
              <h5 className="mb-4">TRANSACTION HISTORY</h5>
            </TabPane>
          </TabContent>
        </Col>
      </Row>
    );
  }
}

export default Profile;
