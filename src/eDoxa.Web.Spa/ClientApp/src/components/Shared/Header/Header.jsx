import React, { Component, Fragment } from "react";
import { LinkContainer } from "react-router-bootstrap";
import { Button, Form, DropdownItem, DropdownMenu, DropdownToggle, UncontrolledDropdown, Nav, NavItem, NavLink } from "reactstrap";
import { AppNavbarBrand, AppSidebarToggler, AppAsideToggler } from "@coreui/react";
import logo from "assets/images/brand/logo.svg";
import sygnet from "assets/images/brand/sygnet.svg";
import { connectUser } from "store/root/user/container";
import userManager, { POST_LOGIN_REDIRECT_URI } from "store/middlewares/oidc/userManager";

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
          {process.env.NODE_ENV !== "production" ? (
            <DropdownItem className="border-top" onClick={() => this.userInfoClickHandled()}>
              User Info
            </DropdownItem>
          ) : null}
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
    const { isAuthenticated, user, balance, children, ...attributes } = this.props;

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

export default connectUser(Header);
