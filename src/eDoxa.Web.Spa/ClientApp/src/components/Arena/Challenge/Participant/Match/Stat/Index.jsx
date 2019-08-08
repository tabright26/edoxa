import React from "react";
import { Modal, Button, Table } from "react-bootstrap";

import Format from "../../../../../../containers/Shared/Formats";

import Loading from "../../../../../../containers/Shared/Loading";

const Stats = props => {
  const { stats } = props;
  return (
    <Modal {...props} size="lg" aria-labelledby="statsModal" centered>
      <Modal.Header closeButton>
        <Modal.Title id="statsModal">Match stats</Modal.Title>
      </Modal.Header>
      {!stats ? (
        <Modal.Body>
          <Loading />
        </Modal.Body>
      ) : (
        <Table className="mb-0" striped>
          <thead>
            <tr>
              <th>Name</th>
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
                <td>{stat.name}</td>
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
              <th colSpan={5}>Score</th>
              <th className="text-center text-primary">
                <Format.Score score={stats.reduce((totalScore, stat) => totalScore + stat.score, 0)} />
              </th>
            </tr>
          </tbody>
        </Table>
      )}
      <Modal.Footer>
        <Button onClick={props.onHide} className="mr-auto">
          Close
        </Button>
      </Modal.Footer>
    </Modal>
  );
};

export default Stats;
