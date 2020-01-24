import React, { Component } from "react";
import {
  Nav,
  Button,
  UncontrolledDropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
  Form
} from "reactstrap";
import { ApplicationPaths } from "./ApiAuthorizationConstants";
import { LinkContainer } from "react-router-bootstrap";
import authorizeService from "utils/oidc/AuthorizeService";
import { DOXATAG_CLAIM_TYPE, EMAIL_CLAIM_TYPE } from "./types";

class LoginMenu extends Component {
  constructor(props) {
    super(props);

    this.state = {
      isAuthenticated: false,
      doxatag: null
    };
  }

  componentDidMount() {
    this._subscription = authorizeService.subscribe(() => this.populateState());
    this.populateState();
  }

  componentWillUnmount() {
    authorizeService.unsubscribe(this._subscription);
  }

  async populateState() {
    const [isAuthenticated, user] = await Promise.all([
      authorizeService.isAuthenticated(),
      authorizeService.getUser()
    ]);
    this.setState({
      isAuthenticated,
      user
    });
  }

  render() {
    const { isAuthenticated, user } = this.state;
    if (!isAuthenticated) {
      const registerPath = `${ApplicationPaths.Register}`;
      const loginPath = `${ApplicationPaths.Login}`;
      return this.anonymousView(registerPath, loginPath);
    } else {
      const profilePath = `${ApplicationPaths.Profile}`;
      const logoutPath = {
        pathname: `${ApplicationPaths.LogOut}`,
        state: { local: true }
      };
      return this.authenticatedView(user, profilePath, logoutPath);
    }
  }

  authenticatedView(user, profilePath, logoutPath) {
    return (
      <Nav className="bg-gray-900 px-3 h-100" navbar>
        <UncontrolledDropdown nav direction="down">
          <DropdownToggle nav caret className="text-center">
            {user[DOXATAG_CLAIM_TYPE] || user[EMAIL_CLAIM_TYPE]}
          </DropdownToggle>
          <DropdownMenu
            right
            style={{
              width: 301,
              top: 35,
              right: -16,
              borderBottomRightRadius: 0,
              borderTopRightRadius: 0,
              borderTopLeftRadius: 0
            }}
          >
            <Form inline>
              <LinkContainer to={profilePath}>
                <Button block color="primary" className="m-3">
                  Profile
                </Button>
              </LinkContainer>
            </Form>
            {process.env.NODE_ENV !== "production" && (
              <DropdownItem
                className="border-top"
                onClick={() => console.log(user)}
              >
                User Info
              </DropdownItem>
            )}
            <LinkContainer to={logoutPath}>
              <DropdownItem>Logout</DropdownItem>
            </LinkContainer>
          </DropdownMenu>
        </UncontrolledDropdown>
      </Nav>
    );
  }

  anonymousView(registerPath, loginPath) {
    return (
      <Nav className="ml-auto mr-3" navbar>
        <LinkContainer to={loginPath}>
          <Button
            size="sm"
            color="link"
            style={{ textDecoration: "none" }}
            className="mr-2"
          >
            Login
          </Button>
        </LinkContainer>
        <Button href={registerPath} size="sm" tag="a" color="primary" outline>
          Register
        </Button>
      </Nav>
    );
  }
}

export default LoginMenu;
