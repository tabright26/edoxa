import React, { FunctionComponent } from "react";
import { Row, Col, ColProps } from "reactstrap";
import ChallengeItem from "./Item";
import { Loading } from "components/Shared/Loading";
import { compose } from "recompose";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { HocUserProfileUserIdStateProps } from "utils/oidc/containers/types";
import {
  CurrencyType,
  CURRENCY_TYPE_MONEY,
  CURRENCY_TYPE_TOKEN
} from "types/cashier";
import { Challenge, CHALLENGE_STATE_INSCRIPTION } from "types/challenges";
import { UserId } from "types/identity";

type OwnProps = HocUserProfileUserIdStateProps & { history?: boolean };

type StateProps = { challenges: Challenge[]; loading: boolean };

type InnerProps = StateProps;

type OutterProps = ColProps & {
  userId?: UserId;
};

type Props = InnerProps & OutterProps;

const lanetsChallenges = (
  challenges: Challenge[],
  currencyType: CurrencyType,
  currencyAmount: number,
  length: number = 1
): Challenge[] => {
  return challenges
    .filter(
      challenge =>
        challenge.entries !== 30 &&
        challenge.payout.entryFee.type.toUpperCase() ===
          currencyType.toUpperCase() &&
        challenge.payout.entryFee.amount === currencyAmount &&
        challenge.state.toUpperCase() ===
          CHALLENGE_STATE_INSCRIPTION.toUpperCase()
    )
    .sort(
      (left, right) =>
        right.participants.length - left.participants.length ||
        (left.name.startsWith("DREAMHACK -")
          ? right.name.startsWith("DREAMHACK -")
            ? 0
            : -1
          : 1) ||
        right.entries - left.entries ||
        right.payout.entryFee.amount - left.payout.entryFee.amount
    )
    .filter((x, index) => index < length);
};

const List: FunctionComponent<Props> = ({
  challenges,
  loading,
  xs,
  sm,
  md,
  lg,
  xl
}) =>
  loading ? (
    <Loading />
  ) : (
    <Row>
      {challenges.map((challenge, index) => (
        <Col key={index} xs={xs} sm={sm} md={md} lg={lg} xl={xl}>
          <ChallengeItem challenge={challenge} />
        </Col>
      ))}
    </Row>
  );

const filterChallenges = (challenges: Challenge[]) => {
  let items: Challenge[] = [];
  const fiveDollars = lanetsChallenges(challenges, CURRENCY_TYPE_MONEY, 5);
  items = [...items, ...fiveDollars];
  const twoDollars = lanetsChallenges(challenges, CURRENCY_TYPE_MONEY, 2);
  items = [...items, ...twoDollars];
  const threeDollars = lanetsChallenges(challenges, CURRENCY_TYPE_MONEY, 3);
  items = [...items, ...threeDollars];
  const twoHundredFiftyTokens = lanetsChallenges(
    challenges,
    CURRENCY_TYPE_TOKEN,
    250,
    2
  );
  items = [...items, ...twoHundredFiftyTokens];
  const fiftyDollars = lanetsChallenges(challenges, CURRENCY_TYPE_MONEY, 50);
  items = [...items, ...fiftyDollars];
  return items;
};

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (
  state,
  ownProps
) => {
  const { data, loading } = state.root.challenge;
  const challenges = ownProps.userId
    ? data.filter(challenge =>
        challenge.participants.some(
          participant => participant.userId === ownProps.userId
        )
      )
    : filterChallenges(data);
  return {
    challenges,
    loading
  };
};

const enhance = compose<InnerProps, OutterProps>(connect(mapStateToProps));

export default enhance(List);
