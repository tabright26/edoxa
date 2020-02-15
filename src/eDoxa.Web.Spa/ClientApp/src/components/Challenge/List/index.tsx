import React, { FunctionComponent } from "react";
import { Row, Col } from "reactstrap";
import ChallengeItem from "./Item";
import { Loading } from "components/Shared/Loading";
import { compose } from "recompose";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import produce, { Draft } from "immer";
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

type OutterProps = {
  userId?: UserId;
  xs: string;
  sm: string;
  md: string;
  lg: string;
};

type Props = InnerProps & OutterProps;

const lanetsChallenges = (
  challenges: Challenge[],
  currencyType: CurrencyType
): Challenge => {
  return (
    challenges
      .filter(
        challenge =>
          challenge.entries !== 30 &&
          challenge.payout.entryFee.type.toUpperCase() ===
            currencyType.toUpperCase() &&
          challenge.state.toUpperCase() ===
            CHALLENGE_STATE_INSCRIPTION.toUpperCase()
      )
      .sort(
        (left, right) =>
          right.participants.length - left.participants.length ||
          (left.name.startsWith("LAN ETS -")
            ? right.name.startsWith("LAN ETS -")
              ? 0
              : -1
            : 1) ||
          right.entries - left.entries ||
          right.payout.entryFee.amount - left.payout.entryFee.amount
      )[0] || null
  );
};

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
            participant => participant.userId === ownProps.userId
          )
        )
      )
    : produce(data, (draft: Draft<Challenge[]>) => {
        const items: Challenge[] = [];
        const money = lanetsChallenges(draft, CURRENCY_TYPE_MONEY);
        if (money) {
          items.push(money);
        }
        const token = lanetsChallenges(draft, CURRENCY_TYPE_TOKEN);
        if (token) {
          items.push(token);
        }
        return items;
      });
  return {
    challenges,
    loading
  };
};

const enhance = compose<InnerProps, OutterProps>(connect(mapStateToProps));

export default enhance(List);
