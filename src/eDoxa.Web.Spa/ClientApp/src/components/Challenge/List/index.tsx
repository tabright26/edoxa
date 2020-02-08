import React, { FunctionComponent } from "react";
import { Row, Col } from "reactstrap";
import ChallengeItem from "./Item";
import { Loading } from "components/Shared/Loading";
import { compose } from "recompose";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { Challenge, UserId } from "types";
import produce, { Draft } from "immer";
import { HocUserProfileUserIdStateProps } from "utils/oidc/containers/types";

type OwnProps = HocUserProfileUserIdStateProps & { history?: boolean };

type StateProps = { challenges: Challenge[]; loading: boolean };

type InnerProps = StateProps;

type OutterProps = {
  userId?: UserId;
  xs: string;
  sm: string;
  md: string;
  lg: string;
};

type Props = InnerProps & OutterProps;

const List: FunctionComponent<Props> = ({
  challenges,
  loading,
  xs,
  sm,
  md,
  lg
}) =>
  loading ? (
    <Loading />
  ) : (
    <Row>
      {challenges.map((challenge, index) => (
        <Col key={index} xs={xs} sm={sm} md={md} lg={lg}>
          <ChallengeItem challenge={challenge} />
        </Col>
      ))}
    </Row>
  );

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (
  state,
  ownProps
) => {
  const { data, loading } = state.root.challenge;
  const challenges = ownProps.userId
    ? produce(data, (draft: Draft<Challenge[]>) =>
        draft.filter(challenge =>
          challenge.participants.some(
            participant => participant.user.id === ownProps.userId
          )
        )
      )
    : data;
  return {
    challenges,
    loading
  };
};

const enhance = compose<InnerProps, OutterProps>(connect(mapStateToProps));

export default enhance(List);
