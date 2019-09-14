import React from "react";
import { Container, CardImg, CardImgOverlay, CardText, CardTitle, Row, Col, Card, Button } from "reactstrap";

import withUserGameHoc from "../../../../containers/withUserGameHoc";

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
            <CardImg />
            <CardImgOverlay>
              <CardTitle as="h5" className="text-center">
                {game.name}
              </CardTitle>
              <CardText>This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.</CardText>
              <CardText>Last updated 3 mins ago</CardText>
              <Button size="lg" block>
                Link an account
              </Button>
            </CardImgOverlay>
          </Card>
        </Col>
      ))}
    </Row>
  </Container>
);

export default withUserGameHoc(UserGameIndex);
