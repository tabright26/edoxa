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
import { ApplicationPaths } from "utils/oidc/ApiAuthorizationConstants";
import { LinkContainer } from "react-router-bootstrap";
import authorizeService from "utils/oidc/AuthorizeService";
import { DOXATAG_CLAIM_TYPE, EMAIL_CLAIM_TYPE } from "utils/oidc/types";
import {
  getProfileTransactionHistoryPath,
  getProfileChallengeHistoryPath,
  getProfilePaymentMethodsPath,
  getProfileGamesPath,
  getProfilePromotionPath,
  getAccountRegisterPath,
  getProfileOverviewPath
} from "utils/coreui/constants";

class AppDropdown extends Component<any, any> {
  _subscription: number;
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
      const loginPath = `${ApplicationPaths.Login}`;
      return this.anonymousView(loginPath);
    } else {
      const logoutPath = {
        pathname: `${ApplicationPaths.LogOut}`,
        state: { local: true }
      };
      return this.authenticatedView(user, logoutPath);
    }
  }

  authenticatedView(user, logoutPath) {
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
              <LinkContainer to={getProfileOverviewPath()}>
                <Button block color="primary" className="m-3">
                  Profile
                </Button>
              </LinkContainer>
            </Form>
            <DropdownItem
              className="border border-left-0 border-right-0"
              header
            >
              Arena
            </DropdownItem>
            <LinkContainer to={getProfileChallengeHistoryPath()}>
              <DropdownItem>Challenge History</DropdownItem>
            </LinkContainer>
            <DropdownItem header>Cashier</DropdownItem>
            <LinkContainer to={getProfilePaymentMethodsPath()}>
              <DropdownItem>Payment Methods</DropdownItem>
            </LinkContainer>
            <LinkContainer to={getProfileTransactionHistoryPath()}>
              <DropdownItem>Transaction History</DropdownItem>
            </LinkContainer>
            <LinkContainer to={getProfilePromotionPath()}>
              <DropdownItem>Promotion</DropdownItem>
            </LinkContainer>
            <DropdownItem header>Connections</DropdownItem>
            <LinkContainer to={getProfileGamesPath()}>
              <DropdownItem>Games</DropdownItem>
            </LinkContainer>
            <DropdownItem header />
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

  anonymousView(loginPath) {
    return (
      <Nav className="ml-auto mr-3" navbar>
        <LinkContainer to={loginPath}>
          <Button size="sm" color="link" className="mr-2">
            Login
          </Button>
        </LinkContainer>
        <LinkContainer to={getAccountRegisterPath()}>
          <Button size="sm" color="primary" outline>
            Register
          </Button>
        </LinkContainer>
      </Nav>
    );
  }
}

export default AppDropdown;
