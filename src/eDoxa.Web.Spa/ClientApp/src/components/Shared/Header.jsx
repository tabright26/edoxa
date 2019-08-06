import React from 'react';
import { connect } from 'react-redux';
import { Navbar, Nav, Form, NavDropdown, Button, Badge } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';
import Moment from 'react-moment';
import userManager, { POST_LOGIN_REDIRECT_URI } from '../../utils/userManager';

class Header extends React.Component {
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
      <Navbar variant="dark" bg="secondary" fixed="top">
        <LinkContainer to="/">
          <Navbar.Brand>eDoxa</Navbar.Brand>
        </LinkContainer>
        {!user ? (
          <Form className="ml-auto" inline>
            <Button variant="link">Register</Button>
            <Navbar.Text className="mr-3">|</Navbar.Text>
            <Button
              variant="primary"
              size="sm"
              onClick={this.signinRedirectClickHandled}
            >
              Login
            </Button>
          </Form>
        ) : (
          <Nav className="ml-auto">
            <Navbar.Text className="mr-5">
              <Badge variant="primary">
                <Moment utc={true} interval={1000} format="LL LTS" />
              </Badge>
            </Navbar.Text>
            <NavDropdown alignRight={true} title={user.profile.name}>
              <NavDropdown.Header>
                <LinkContainer to="/profile">
                  <Button active block>
                    Profile
                  </Button>
                </LinkContainer>
              </NavDropdown.Header>
              <NavDropdown.Header>Arena</NavDropdown.Header>
              <LinkContainer to="/arena/challenge-history">
                <NavDropdown.Item eventKey="1">
                  Challenge History
                </NavDropdown.Item>
              </LinkContainer>
              <NavDropdown.Divider />
              <NavDropdown.Header>Cashier</NavDropdown.Header>
              <LinkContainer to="/account/overview">
                <NavDropdown.Item eventKey="2">
                  Account Overview
                </NavDropdown.Item>
              </LinkContainer>
              <LinkContainer to="/transaction-history">
                <NavDropdown.Item eventKey="3">
                  Transaction History
                </NavDropdown.Item>
              </LinkContainer>
              <LinkContainer to="/payment-methods">
                <NavDropdown.Item eventKey="4">
                  Payment Methods
                </NavDropdown.Item>
              </LinkContainer>
              <NavDropdown.Divider />
              <NavDropdown.Header>DevOnly</NavDropdown.Header>
              <NavDropdown.Item
                eventKey="5"
                onClick={this.userInfoClickHandled}
              >
                User Info
              </NavDropdown.Item>
              <NavDropdown.Divider />
              <NavDropdown.Item
                eventKey="6"
                onClick={this.signoutRedirectClickHandled}
              >
                Logout
              </NavDropdown.Item>
            </NavDropdown>
          </Nav>
        )}
      </Navbar>
    );
  }
}

const mapStateToProps = state => {
  return {
    user: state.oidc.user
  };
};

export default connect(mapStateToProps)(Header);
