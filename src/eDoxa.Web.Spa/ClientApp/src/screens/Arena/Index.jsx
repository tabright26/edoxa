import React from 'react';
import { Container, Row, Col, Card } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';

import logo from '../../assets/images/arena.challenges.png';

export default class Home extends React.Component {
  render() {
    return (
      <Container>
        <Row>
          <Col xs={{ span: 4, offset: 4 }}>
            <LinkContainer to="/challenges">
              <Card bg="dark" title="Challenges" className="mt-5">
                <Card.Img
                  variant="top"
                  src={logo}
                  alt="arena.challenges.ico"
                  className="p-4"
                />
                <Card.Footer className="bg-primary">
                  <Card.Text as="h5" className="text-light text-center">
                    Challenges
                  </Card.Text>
                </Card.Footer>
              </Card>
            </LinkContainer>
          </Col>
        </Row>
      </Container>
    );
  }
}
