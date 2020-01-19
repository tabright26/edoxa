import React, { FunctionComponent } from "react";
import { Row, Col, Card, CardImg, CardImgOverlay } from "reactstrap";
import { withChallenges } from "store/root/challenge/container";
import ChallengeItem from "./Item";
import { Loading } from "components/Shared/Loading";
import { ChallengesState } from "store/root/challenge/types";
import banner from "assets/img/arena/games/leagueoflegends/banner.jpg";
import large from "assets/img/arena/games/leagueoflegends/large.png";

interface Props {
  challenges: ChallengesState;
}

const ChallengeList: FunctionComponent<Props> = ({
  challenges: { data, error, loading }
}) => {
  return loading ? (
    <Loading />
  ) : (
    <>
      <Row>
        <Col>
          <Card className="my-4">
            <CardImg src={banner} height="200" />
            <CardImgOverlay className="d-flex">
              <img
                className="m-auto"
                alt="leagueoflegends"
                src={large}
                width={320}
                height={125}
              />
            </CardImgOverlay>
          </Card>
        </Col>
      </Row>
      <Row>
        {data.map((challenge, index) => (
          <Col key={index} xs="12" sm="12" md="12" lg="12">
            <ChallengeItem challenge={challenge} />
          </Col>
        ))}
      </Row>
    </>
  );
};

export default withChallenges(ChallengeList);
