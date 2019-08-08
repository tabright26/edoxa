import React, { Component, Fragment } from "react";
import { Nav } from "reactstrap";
import { LinkContainer } from "react-router-bootstrap";
import { AppNavbarBrand, AppSidebarToggler } from "@coreui/react";

import withUserContainer from "../../../containers/App/User/Containers";

import logo from "../../../assets/img/brand/logo.svg";
import sygnet from "../../../assets/img/brand/sygnet.svg";

import { Button, Form, DropdownItem, DropdownMenu, DropdownToggle, UncontrolledDropdown } from "reactstrap";

import userManager, { POST_LOGIN_REDIRECT_URI } from "../../../utils/userManager";

class HeaderDropdown extends Component {
  signinRedirectClickHandled = event => {
    event.preventDefault();
    userManager.signinRedirect();
  };
  signoutRedirectClickHandled = event => {
    event.preventDefault();
    localStorage.removeItem(POST_LOGIN_REDIRECT_URI);
    userManager.removeUser();
    userManager.signoutRedirect();
  };

  userInfoClickHandled = event => {
    event.preventDefault();
    const { user } = this.props;
    console.log(JSON.stringify(user, null, 2));
  };
  render() {
    const { user } = this.props;

    return (
      <UncontrolledDropdown nav direction="down">
        <DropdownToggle nav caret>
          {user ? user.profile.email : ""}
        </DropdownToggle>
        <DropdownMenu right style={{ right: 0 }}>
          <Form inline>
            <LinkContainer to="/profile">
              <Button block size="sm" color="primary" className="m-3">
                Profile
              </Button>
            </LinkContainer>
          </Form>
          <DropdownItem header>Connections</DropdownItem>
          <LinkContainer to="/user/games">
            <DropdownItem>My Games</DropdownItem>
          </LinkContainer>
          <DropdownItem header>Cashier</DropdownItem>
          <LinkContainer to="/account/overview">
            <DropdownItem>Account Overview</DropdownItem>
          </LinkContainer>
          <LinkContainer to="/account/transaction-history">
            <DropdownItem>Transaction History</DropdownItem>
          </LinkContainer>
          <LinkContainer to="/account/payment-methods">
            <DropdownItem>Payment Methods</DropdownItem>
          </LinkContainer>
          <DropdownItem header>Arena</DropdownItem>
          <LinkContainer to="/arena/challenge-history">
            <DropdownItem>Challenge History</DropdownItem>
          </LinkContainer>
          <LinkContainer to="/arena/tournament-history">
            <DropdownItem>Tournament History</DropdownItem>
          </LinkContainer>
          <DropdownItem divider />
          <DropdownItem onClick={this.signinRedirectClickHandled}>Login</DropdownItem>
          <DropdownItem onClick={this.userInfoClickHandled}>User Info</DropdownItem>
          <DropdownItem onClick={this.signoutRedirectClickHandled}>Logout</DropdownItem>
        </DropdownMenu>
      </UncontrolledDropdown>
    );
  }
}

class Header extends Component {
  render() {
    // eslint-disable-next-line
    const { isAuthenticated, user, children, ...attributes } = this.props;

    return (
      <Fragment>
        <AppSidebarToggler className="d-lg-none" display="md" mobile />
        <LinkContainer to="/">
          <AppNavbarBrand full={{ src: logo, width: 89, height: 25, alt: "eDoxa Logo" }} minimized={{ src: sygnet, width: 30, height: 30, alt: "eDoxa Logo" }} />
        </LinkContainer>
        <AppSidebarToggler className="d-md-down-none" display="lg" />
        <Nav className="ml-auto mr-3" navbar>
          <HeaderDropdown user={user} />
        </Nav>
        {/* <AppAsideToggler className="d-md-down-none" /> */}
      </Fragment>
    );
  }
}

export default withUserContainer(Header);
