import React from "react";
import { Container, Row, Col, Card, Button } from "react-bootstrap";

import withUserGameHoc from "../../../../withUserGameHoc";

const UserGameIndex = ({ games }) => (
  <Container>
    <Row>
      {games.map((game, index) => (
        <Col key={index} xl="4">
          <Card
            className="my-3"
            style={{
              height: "300px"
            }}
          >
            <Card.Img />
            <Card.ImgOverlay>
              <Card.Title as="h5" className="text-center">
                {game.name}
              </Card.Title>
              <Card.Text>This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.</Card.Text>
              <Card.Text>Last updated 3 mins ago</Card.Text>
              <Button size="lg" block>
                Link an account
              </Button>
            </Card.ImgOverlay>
          </Card>
        </Col>
      ))}
    </Row>
  </Container>
);

export default withUserGameHoc(UserGameIndex);
