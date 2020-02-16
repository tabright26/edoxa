import React, { FunctionComponent, useEffect } from "react";
import ChallengeList from "components/Service/Challenge/List";
import { loadChallenges } from "store/actions/challenge";
import { connect, MapDispatchToProps } from "react-redux";
import { compose } from "recompose";
import { Row, Col, Card, CardImg, CardImgOverlay } from "reactstrap";
import banner from "assets/img/arena/games/leagueoflegends/banner.jpg";
import large from "assets/img/arena/games/leagueoflegends/large.png";

type OwnProps = {};

type DispatchProps = {
  loadChallenges: () => void;
};

type InnerProps = DispatchProps;

type OutterProps = OwnProps;

type Props = InnerProps & OutterProps;

const Challenges: FunctionComponent<Props> = ({ loadChallenges }) => {
  useEffect((): void => {
    loadChallenges();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  return (
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
      <ChallengeList xs="12" sm="12" md="12" lg="6" />
    </>
  );
};

const mapDispatchToProps: MapDispatchToProps<DispatchProps, OwnProps> = (
  dispatch: any
) => {
  return {
    loadChallenges: () => dispatch(loadChallenges())
  };
};

const enhance = compose<InnerProps, OutterProps>(
  connect(null, mapDispatchToProps)
);

export default enhance(Challenges);
