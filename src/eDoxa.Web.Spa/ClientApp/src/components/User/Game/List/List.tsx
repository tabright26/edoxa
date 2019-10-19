import React, { FunctionComponent } from "react";
import { CardHeader, CardImg, CardImgOverlay, CardText, Row, Col, Card, Button } from "reactstrap";
import Loading from "components/Shared/Override/Loading";
import { withUserGames } from "store/root/user/games/container";
import { compose } from "recompose";

const UserGameList: FunctionComponent<any> = ({ games: { data, error, loading } }) =>
  loading ? (
    <Loading />
  ) : (
    <Row>
      {data.map((game, index) => (
        <Col key={index} xl="4">
          <Card className="card-accent-primary my-3">
            <CardHeader>
              <strong className="text-uppercase">{game.name}</strong>
            </CardHeader>
            <Card
              style={{
                height: "225px"
              }}
              className="mb-0 border-0"
            >
              <CardImg />
              <CardImgOverlay>
                <CardText>This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.</CardText>
                <CardText>Last updated 3 mins ago</CardText>
                <Button color="primary" block>
                  Link an account
                </Button>
              </CardImgOverlay>
            </Card>
          </Card>
        </Col>
      ))}
    </Row>
  );

const enhance = compose<any, any>(withUserGames);

export default enhance(UserGameList);
