import React, { FunctionComponent } from "react";
import { connectModal, InjectedProps } from "redux-modal";
import { Modal, ModalFooter, ModalHeader, Table, Button } from "reactstrap";
import Format from "components/Shared/Format";
import { CHALLENGE_MATCH_SCORE_MODAL } from "utils/modal/constants";
import { compose } from "recompose";
import { sentenceCase } from "change-case";
import { ChallengeMatchStat } from "types/challenges";

type InnerProps = InjectedProps & { stats: ChallengeMatchStat[] };

type OutterProps = {};

type Props = InnerProps & OutterProps;

const Score: FunctionComponent<Props> = ({ show, handleHide, stats }) => (
  <Modal size="lg" backdrop="static" isOpen={show} toggle={handleHide} centered>
    <ModalHeader toggle={handleHide} className="text-uppercase my-auto">
      Score Details
    </ModalHeader>
    <Table className="mb-0" size="sm" responsive striped dark>
      <thead>
        <tr>
          <th className="pl-3">Name</th>
          <th className="text-center">Value</th>
          <th className="text-center" />
          <th className="text-center">Weighting</th>
          <th className="text-center" />
          <th className="text-center">Score</th>
        </tr>
      </thead>
      <tbody>
        {stats.map((stat, index) => (
          <tr key={index}>
            <td className="pl-3">{sentenceCase(stat.name)}</td>
            <td className="text-center">{stat.value}</td>
            <td className="text-center">&#10005;</td>
            <td className="text-center">{stat.weighting}</td>
            <td className="text-center">&#61;</td>
            <td className="text-center text-primary">
              <Format.Score score={stat.score} />
            </td>
          </tr>
        ))}
        <tr>
          <th colSpan={5} className="pl-3">
            Score
          </th>
          <th className="text-center text-primary">
            <Format.Score
              score={stats.reduce(
                (totalScore, stat) => totalScore + stat.score,
                0
              )}
            />
          </th>
        </tr>
      </tbody>
    </Table>
    <ModalFooter className="d-flex">
      <Button className="w-25 m-auto" color="primary" onClick={handleHide}>
        Close
      </Button>
    </ModalFooter>
  </Modal>
);

const enhance = compose<InnerProps, OutterProps>(
  connectModal({ name: CHALLENGE_MATCH_SCORE_MODAL, destroyOnHide: false })
);

export default enhance(Score);
