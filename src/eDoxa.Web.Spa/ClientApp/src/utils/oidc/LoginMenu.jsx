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
import authService from "./AuthorizeService";
import { ApplicationPaths } from "./ApiAuthorizationConstants";
import { LinkContainer } from "react-router-bootstrap";

export class LoginMenu extends Component {
  constructor(props) {
    super(props);

    this.state = {
      isAuthenticated: false,
      doxatag: null
    };
  }

  componentDidMount() {
    this._subscription = authService.subscribe(() => this.populateState());
    this.populateState();
  }

  componentWillUnmount() {
    authService.unsubscribe(this._subscription);
  }

  async populateState() {
    const [isAuthenticated, user] = await Promise.all([
      authService.isAuthenticated(),
      authService.getUser()
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
      <Nav className="ml-auto mr-3" navbar>
        <UncontrolledDropdown nav direction="down">
          <DropdownToggle nav caret>
            {user["doxatag"] ? user["doxatag"] : user["email"]}
          </DropdownToggle>
          <DropdownMenu right style={{ right: 0 }}>
            <Form inline>
              <LinkContainer to={profilePath}>
                <Button block size="sm" color="primary" className="m-3">
                  Profile
                </Button>
              </LinkContainer>
            </Form>
            {process.env.NODE_ENV !== "production" ? (
              <DropdownItem
                className="border-top"
                onClick={() =>
                  authService.getUser().then(user => console.log(user))
                }
              >
                User Info
              </DropdownItem>
            ) : null}
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
