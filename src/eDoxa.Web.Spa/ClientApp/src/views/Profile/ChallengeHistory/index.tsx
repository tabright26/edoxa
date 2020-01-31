import React, { FunctionComponent, useEffect } from "react";
import { compose } from "recompose";
import { connect, MapDispatchToProps } from "react-redux";
import { loadChallengeHistory } from "store/actions/challenge";
import ChallengeList from "components/Challenge/List";
import {
  withUserProfileUserId,
  HocUserProfileUserIdStateProps
} from "utils/oidc/containers";

type OwnProps = HocUserProfileUserIdStateProps;

type DispatchProps = { loadChallengeHistory: () => void };

type InnerProps = DispatchProps & OwnProps;

type OutterProps = {};

type Props = InnerProps & OutterProps;

const ProfileChallengeHistory: FunctionComponent<Props> = ({
  loadChallengeHistory,
  userId
}) => {
  useEffect((): void => {
    loadChallengeHistory();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  return (
    <>
      <h5 className="text-uppercase my-4">CHALLENGE HISTORY</h5>
      <ChallengeList userId={userId} xs="12" sm="12" md="12" lg="12" />
    </>
  );
};

const mapDispatchToProps: MapDispatchToProps<DispatchProps, OwnProps> = (
  dispatch: any
) => {
  return {
    loadChallengeHistory: () => dispatch(loadChallengeHistory())
  };
};

const enhance = compose<InnerProps, OutterProps>(
  withUserProfileUserId,
  connect(null, mapDispatchToProps)
);

export default enhance(ProfileChallengeHistory);
