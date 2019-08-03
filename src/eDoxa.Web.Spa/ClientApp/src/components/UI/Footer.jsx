import React from 'react';
import { Navbar, Container } from 'react-bootstrap';

export default class Footer extends React.Component {
  render() {
    return (
      <footer>
        <Navbar fixed="bottom" bg="secondary" variant="dark">
          <Container>
            <Navbar.Text className="m-auto">
              <small>
                &copy; {new Date(Date.now()).getFullYear()} eDoxa - All rights
                reserved.
              </small>
            </Navbar.Text>
          </Container>
        </Navbar>
      </footer>
    );
  }
}
