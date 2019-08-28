import React, { Component, Fragment } from "react";
import { LinkContainer } from "react-router-bootstrap";
import { AppNavbarBrand, AppSidebarToggler, AppAsideToggler } from "@coreui/react";
import withUserHoc from "../../../containers/App/User/withUserHoc";

import logo from "../../../assets/img/brand/logo.svg";
import sygnet from "../../../assets/img/brand/sygnet.svg";

import { Button, Form, DropdownItem, DropdownMenu, DropdownToggle, UncontrolledDropdown, Nav, NavItem, NavLink } from "reactstrap";

import userManager, { POST_LOGIN_REDIRECT_URI } from "../../../utils/userManager";

class HeaderDropdown extends Component {
  signinRedirectClickHandled = () => {
    userManager.signinRedirect();
  };

  signoutRedirectClickHandled = () => {
    localStorage.removeItem(POST_LOGIN_REDIRECT_URI);
    userManager.removeUser();
    userManager.signoutRedirect();
  };

  userInfoClickHandled = () => {
    const { user } = this.props;
    console.log(JSON.stringify(user, null, 2));
  };

  render() {
    const { user } = this.props;

    return (
      <UncontrolledDropdown nav direction="down">
        <DropdownToggle nav caret>
          {user.profile.email}
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
          {process.env.NODE_ENV !== "production" ? <DropdownItem onClick={() => this.userInfoClickHandled()}>User Info</DropdownItem> : null}
          <DropdownItem onClick={() => this.signoutRedirectClickHandled()}>Logout</DropdownItem>
        </DropdownMenu>
      </UncontrolledDropdown>
    );
  }
}

class Header extends Component {
  signinRedirectClickHandled = () => {
    userManager.signinRedirect();
  };

  render() {
    // eslint-disable-next-line
    const { isAuthenticated, user, children, ...attributes } = this.props;

    return (
      <Fragment>
        <AppSidebarToggler className="d-lg-none" display="md" mobile />
        <LinkContainer to="/">
          <AppNavbarBrand full={{ src: logo, width: 89, height: 25, alt: "eDoxa Logo" }} minimized={{ src: sygnet, width: 30, height: 30, alt: "eDoxa Logo" }} />
        </LinkContainer>
        <AppSidebarToggler className="d-md-down-none" display="lg" hidden />
        <Nav className="d-md-down-none" navbar>
          <NavItem className="px-3">
            <LinkContainer to="/marketplace">
              <NavLink className="nav-link">Marketplace</NavLink>
            </LinkContainer>
          </NavItem>
          <NavItem className="px-3">
            <LinkContainer to="/news-feeds">
              <NavLink className="nav-link">News Feeds</NavLink>
            </LinkContainer>
          </NavItem>
        </Nav>
        {isAuthenticated ? (
          <Nav className="ml-auto mr-3" navbar>
            <HeaderDropdown user={user} />
          </Nav>
        ) : (
          <Nav className="ml-auto mr-3" navbar>
            <Button size="sm" color="link" style={{ textDecoration: "none" }} className="mr-2" onClick={() => this.signinRedirectClickHandled()}>
              Login
            </Button>
            <Button href={`${process.env.REACT_APP_AUTHORITY}/Identity/Account/Register`} size="sm" tag="a" color="primary" outline>
              Register
            </Button>
          </Nav>
        )}
        <AppAsideToggler className="d-md-down-none" hidden />
      </Fragment>
    );
  }
}

export default withUserHoc(Header);
