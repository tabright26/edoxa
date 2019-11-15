import React, { FunctionComponent } from "react";
import { Row, Col, Card, CardImg, CardImgOverlay } from "reactstrap";
import { withChallenges } from "store/root/arena/challenges/container";
import ChallengeItem from "./Item";
import ErrorBoundary from "components/Shared/ErrorBoundary";
import Loading from "components/Shared/Loading";
import { ChallengesState } from "store/root/arena/challenges/types";

interface Props {
  challenges: ChallengesState;
}

const ChallengeList: FunctionComponent<Props> = ({
  challenges: { data, error, loading }
}) => {
  return (
    <ErrorBoundary>
      {loading ? (
        <Loading />
      ) : (
        <>
          <Row>
            <Col>
              <Card className="my-4">
                <CardImg
                  src="https://via.placeholder.com/1680x200"
                  height="200"
                />
                <CardImgOverlay>
                  <h5>League of Legends</h5>
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
      )}
    </ErrorBoundary>
  );
};

export default withChallenges(ChallengeList);
