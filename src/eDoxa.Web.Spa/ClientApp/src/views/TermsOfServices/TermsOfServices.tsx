import React, { useEffect, FunctionComponent } from "react";
import { Container, Card, CardBody } from "reactstrap";
import { withLocalize } from "react-localize-redux";
import { compose } from "recompose";
import locale from "./locale.json";

import "./TermsOfServices.scss";

// https://ryandrewjohnson.github.io/react-localize-redux-docs/
const TermsOfServices: FunctionComponent<any> = ({ addTranslationForLanguage }) => {
  useEffect(() => {
    addTranslationForLanguage(locale, "en");
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  return (
    <Container>
      <Card className="mt-4">
        <CardBody className="text-justify p-5">
          <h1>Terms of Services</h1>
          <section>
            <article>
              <h3>OVERVIEW – PLAY NICE</h3>
              <p>
                eDoxa strives to provide a fair and equitable system for all our users. We endeavor to treat our clients as we would like to be treated and expect all our clients to treat each other
                with respect and honesty. If you attempt to cheat or break the rules, we will not only remove you from the system, but also ban you and prosecute to the full extend allowable at law,
                which may include civil actions &amp; criminal charges.
              </p>
            </article>
            <article>
              <h3>IMPORTANT LEGAL NOTICE REGARDING TERMS OF USE OF EDOXA.GG</h3>
              <p>IMPORTANT! PLEASE CAREFULLY READ THESE TERMS OF USE BEFORE USING EDOXA.GG, AS THEY AFFECT YOUR LEGAL RIGHTS AND OBLIGATIONS.</p>
            </article>
            <article>
              <h3>USER AGREEMENT</h3>
              <p>
                eDoxa owns and operates the Website that links to these Terms of Use. We are pleased to offer you access to our Website and the ability to participate in our online gaming contests of
                skill, other content, products, services, and promotions (collectively the “Services”) that we may provide from our Website, subject to these Terms of Use (the “Terms of Use”), our
                privacy policy (the “Privacy Policy”) and the Official Rules and Regulations for the applicable contests and promotions (the “Rules” or “Rules and Scoring,” and together with the Terms
                of Use and the Privacy Policy, the “Agreements”).
              </p>
            </article>
            <article>
              <h3>CONSIDERATION</h3>
              <p>
                You agree to these Terms of Use by accessing or using the Website, registering for Services offered on the Website, or by accepting, uploading, submitting or downloading any
                information or content from or to the Website. IF YOU DO NOT AGREE TO BE BOUND BY ALL OF THESE TERMS OF USE, DO NOT USE THE WEBSITE. These Terms of Use constitute a legal agreement
                between you and eDoxa and shall apply to your use of the Website and the Services even after termination.
              </p>
            </article>
            <article>
              <h3>ELIGIBILITY</h3>
              <p>In order to sign up and maintain an account on eDoxa, an individual must:</p>
              <ul>
                <li>be a natural person, at least 14 years old, and who has had the email address submitted on the account sign up form assigned to their use and control as represented;</li>
                <li>be a citizen or resident of a country (&quot;Territory&quot;) where skilled gaming is legal (as applicable from state to state within a given Territory);</li>
                <li>be physically located in a Territory in which participation in the Service you select, or the Site, is unrestricted by applicable laws; and</li>
                <li>comply with these T&amp;Cs at all times.</li>
                <li>
                  Individuals between 14 and 18 must have permission from a parent or legal guardian, who must read these T&amp;Cs and agree to be bound by them, on behalf of their minor child. eDoxa
                  reserves the right to confirm that such permission has been granted and to close or restrict any Accounts until eDoxa is satisfied that a parent or legal guardian has read the
                  T&amp;Cs and agrees to allow their minor child to hold an account.
                </li>
                <li>
                  Certain sections of the Site are age gated to prevent minors from accessing games such as pay-to-enter challenges and tournaments unless permission has been given from parent or
                  legal guardian. Minors may be prohibited from entering tournaments or challenges based on ESRB or other game ratings, or conditions set by eDoxa.
                </li>
                <li>
                  All signed up account holders may play free or non-prizing tournaments on the Site (subject to the age rating of the game). eDoxa employees may use the Website and will from time to
                  time do so for the purpose of testing the site user experience, socializing and competing with customers to build community, and other reasonable and fair uses at the discretion of
                  eDoxa.
                </li>
                <li>
                  Additional Information for U.S. Citizens and Residents: The laws governing contests, tournaments and skilled gaming with entry fees and/or prizes are established by each individual
                  state, not by the federal government. As such, eDoxa CANNOT, and therefore DOES NOT, offer fee-based tournaments or games with prizes to residents of the following states: Louisiana,
                  Montana. VOID WHERE PROHIBITED OR RESTRICTED BY LAW.
                </li>
                <li>
                  If you open an account and/or participate in any tournament or game offered on the Site while located in a prohibited jurisdiction, you will be in violation of the law of such
                  jurisdiction and these T&amp;Cs, and subject to having your account suspended or terminated and all winnings (if any) voided.
                </li>
                <li>
                  You may establish only one account per person to participate in the Services offered on the Website. In the event eDoxa discovers that you have opened more than one account per
                  person, in addition to any other rights that eDoxa may have, eDoxa reserves the right to suspend or terminate any or all of your accounts and terminate, withhold or revoke the
                  awarding of any prizes. You are responsible for maintaining the confidentiality of your login names and passwords and you accept responsibility for all activities, charges, and
                  damages that occur under your account. It shall be a violation of these Terms of Use to allow any other person to use your account to participate in any contest. If you have reason
                  to believe that someone is using your account without your permission, you should contact us immediately at help@edoxa.gg. We will not be responsible for any loss or damage resulting
                  from your failure to notify us of unauthorized use. If we request registration information from you, you must provide us with accurate and complete information and must update the
                  information when it changes.
                </li>
                <li>
                  “Authorized Account Holder” is defined as the natural person from 14 years of age or older who is assigned to an e-mail address by an Internet access provider, on-line service
                  provider, or other organization (e.g., business, education institution, etc.) that is responsible for assigning e-mail addresses for the domain associated with the submitted e-mail
                  address for registration on the Website. By inputting a payment method to participate in real money contests, the Authorized Account Holder hereby affirms that the Authorized Account
                  Holder is the lawful owner of the payment method account used to make any deposit(s) on the Website or the lawful owner of the payment method provided explicit consent to use it. It
                  shall be a violation of these Terms of Use for any Authorized Account Holder to submit payment using any payment method that is not owned by the Authorized Account Holder or provided
                  consent.
                </li>
              </ul>
            </article>
            <article>
              <h3>CONTEST ENTRY</h3>
              <p>Users will be able to visit the Website and view the games available for entry (the “Contests”). Each Challenge that is not free to enter has an entry fee listed in US dollars.</p>
              <p>When you select to participate in a Contest and complete the entry process, the listed amount of US dollars will be debited from your eDoxa account.</p>
            </article>
            <article>
              <h3>REFUND POLICY</h3>
              <p>All payments are final. No refunds will be issued particularly for any funds lost in skilled gaming Challenges or tournaments. eDoxa’s discretion is absolute.</p>
            </article>
            <article>
              <h3>CONDITIONS OF PARTICIPATION</h3>
              <p>
                By entering a Contest, entrants agree to be bound by these Rules and the decisions of eDoxa, which shall be final and binding in all respects. The Company, at its sole discretion, may
                disqualify any entrant from a Contest, refuse to award benefits or prizes and require the return of any prizes, if the entrant engages in conduct the Company deems to be improper,
                unfair or otherwise adverse to the operation of the Contest or is in any way detrimental to other entrants. Improper conduct includes, but is not limited to:
              </p>
              <ul>
                <li>Falsifying personal information required to enter a Contest or claim a prize;</li>
                <li>Engaging in any type of financial fraud including unauthorized use of credit instruments to enter a Contest or claim a prize;</li>
                <li>With the exception of team events, colluding with any other individual(s) or engaging in any type of syndicate play;</li>
                <li>Any violation of Contest rules or the Terms of Use;</li>
                <li>
                  Using automated means (including but not limited to harvesting bots, robots, parser, spiders or screen scrapers) to obtain, collect or access any information on the Website or of any
                  User for any purpose.
                </li>
                <li>Any type of bonus abuse, abuse of the refer-a-friend program, or abuse of any other offers or promotions;</li>
                <li>
                  Using so-called “Smurf” accounts (accounts that are operated in addition to the main account and whose rank is deliberately kept low to compete against weaker players online) on the
                  platform. Which are either reported by other users or stand out for other reasons.
                </li>
                <li>Attaching GameID’s to your account that are not owned by the eDoxa account holder.</li>
                <li>Tampering with the administration of a Contest or trying to in any way tamper with the computer programs or any security measure associated with a Contest;</li>
                <li>Obtaining other entrants information and spamming other entrants; or  Abusing the Website in any way.</li>
              </ul>
              <p>
                eDoxa reserves the right to close or suspend the Accounts of, and void all account balances and entries by any person where, in eDoxa’s reasonable opinion, the Account has not been
                operated with integrity, followed eDoxa Conditions of Participation and/or the matches had not been played on a good faith basis. eDoxa can in its sole discretion, withhold any or all
                related funds in the Account pending the outcome of an investigation on that Account. Users further acknowledge that the forfeiture and/or return of any prize shall in no way prevent
                eDoxa from pursuing criminal or civil proceedings in connection with such conduct.
              </p>
              <p>
                By entering into a Contest or accepting any prize, entrants, including but not limited to the winner(s), agree to indemnify, release and to hold harmless eDoxa, its parents,
                subsidiaries, affiliates and agents, as well as the officers, directors, employees, shareholders and representatives of any of the foregoing entities (collectively, the “Released
                Parties”), from any and all liability, claims or actions of any kind whatsoever, including but not limited to injuries, damages, or losses to persons and property which may be
                sustained in connection with participation in the Contest, the receipt, ownership, use or misuse of any prize or while preparing for, participating in and/or travelling to or from any
                prize related activity, as well as any claims based on publicity rights, defamation, or invasion of privacy. eDoxa may, in its sole and absolute discretion, require an Authorized
                Account Holder to execute a separate release of claims similar to the one listed above in this Paragraph as a condition of being awarded any prize or receiving any payout.
              </p>
              <p>
                eDoxa is not responsible for: any incorrect, invalid or inaccurate entry information; human errors; postal delays/postage due mail; technical malfunctions; failures, including public
                utility or telephone outages; omissions, interruptions, deletions or defects of any telephone system or network, computer online systems, data, computer equipment, servers, providers,
                or software (including, but not limited to software and operating systems that do not permit an entrant to participate in a Contest), including without limitation any injury or damage
                to any entrant’s or any other person’s computer or video equipment relating to or resulting from participation in a Contest; inability to access the Website, or any web pages that are
                part of or related to the Website; theft, tampering, destruction, or unauthorized access to, or alteration of, entries and/or images of any kind; data that is processed late or
                incorrectly or is incomplete or lost due to telephone, postal issues, computer or electronic malfunction or traffic congestion on telephone lines or transmission systems, or the
                Internet, or any service provider’s facilities, or any phone site or website or for any other reason whatsoever; typographical, printing or other errors, or any combination thereof.
              </p>
              <p>
                eDoxa is not responsible for incomplete, illegible, misdirected or stolen entries. If for any reason a Contest is not capable of running as originally planned, or if a Contest,
                computer application, or website associated therewith (or any portion thereof) becomes corrupted or does not allow the proper entry to a Contest in accordance with the Terms of Use or
                applicable Contest rules, or if infection by a computer (or similar) virus, bug, tampering, unauthorized intervention, actions by entrants, fraud, technical failures, or any other
                causes of any kind, in the sole opinion of eDoxa corrupts or affects the administration, security, fairness, integrity, or proper conduct of a Contest, the Company reserves the right,
                at its sole discretion, to disqualify any individual implicated in such action and/or to cancel, terminate, extend, modify or suspend the Contest, and select the winner(s) from all
                eligible entries received. If such cancellation, termination, modification or suspension occurs, notification will be posted on the Website.
              </p>
              <p>
                ANY ATTEMPT BY AN ENTRANT OR ANY OTHER INDIVIDUAL TO DELIBERATELY DAMAGE THE WEBSITE OR UNDERMINE THE LEGITIMATE OPERATION OF ANY CONTEST IS A VIOLATION OF CRIMINAL AND/OR CIVIL LAWS
                AND SHOULD SUCH AN ATTEMPT BE MADE, EDOXA RESERVES THE RIGHT TO SEEK DAMAGES AND OTHER REMEDIES FROM ANY SUCH PERSON TO THE FULLEST EXTENT PERMITTED BY LAW.
              </p>
              <p>All entries become the property of eDoxa and will not be acknowledged or returned.</p>
              <p>
                To be eligible to enter any contest or receive any prize, the Authorized Account Holder may be required to provide eDoxa with additional documentation and/or information to verify the
                identity of the Authorized Account Holder, and to provide proof that all eligibility requirements are met. In the event of a dispute as to the identity or eligibility of an Authorized
                Account Holder, eDoxa will, in its sole and absolute discretion, utilize certain information collected by eDoxa to assist in verifying the identity and/or eligibility of such
                Authorized Account Holder.
              </p>
              <p>
                Participation in each Contest must be made only as specified in the Terms of Use. Failure to comply with these Terms of Use will result in disqualification and, if applicable, prize
                forfeiture.
              </p>
              <p>
                Where legal, both entrants and winner consent to the use of their name, voice, and likeness/photograph in and in connection with the development, production, distribution and/or
                exploitation of any Contest or the Website. Winners agree that from the date of notification by eDoxa of their status as a potential winner and continuing until such time when eDoxa
                informs them that they no longer need to do so that they will make themselves available to eDoxa for publicity, advertising, and promotion activities.
              </p>
              <p>eDoxa reserves the right to move entrants from the Contests they have entered to substantially similar Contests in certain situations determined by eDoxa in its sole discretion.</p>
            </article>
            <article>
              <h3>CONTEST PRIZES AND PROMOTIONS</h3>
              <p>
                Prizes will only be awarded if a Contest is run. We reserve the right to cancel Contests at any time. In the event of a cancellation, all entry fees will be refunded to the customer
                except as specifically provided in these Terms of Use.
              </p>
              <p>
                Guaranteed prizes are offered in connection with some of the Contests offered by the Website. Each Contest or promotion is governed by its own set of official rules. We encourage you
                to read such Contest and promotions Rules before participating.
              </p>
            </article>
            <article>
              <h3>CONTEST OF SKILL</h3>
              <p>
                Contests offered on the Website are contests of skill. Winners are determined by the objective criteria described in the Contest deadline, roster, Rules, scoring, and any other
                applicable documentation associated with the Contest. From all entries received for each Contest, winners are determined by the individuals who use their skill and knowledge of
                relevant sports information and fantasy sports rules to accumulate the most points according to the corresponding scoring rules. The Website and Contests may not be used for any form
                of illicit gambling.
              </p>
            </article>
            <article>
              <h3>CONTEST RESULTS</h3>
              <p>
                Contest results and prize calculations are based on the final statistics and scoring results at the completion of each individual Contest. Once Contest results are reviewed and graded,
                prizes are awarded. The scoring results of a Contest will not be changed regardless of any official statistics or scoring adjustments made at later times or dates, except in eDoxa sole
                discretion.
              </p>
              <p>eDoxa reserves the right, in its sole and absolute discretion, to deny any contestant the ability to participate in Challenges for any reason whatsoever.</p>
            </article>
            <article>
              <h3>PRIZES</h3>
              <p>
                At the conclusion of each Contest, prizes will be immediately awarded except in circumstances where technical failure or other reasons prevent such timely payout. Prizes won are added
                to the winning participants account balance. In the event of a tie, prizes are divided evenly amongst the participants that have tied.
              </p>
              <p>
                eDoxa reserves the right, in its sole discretion, to cancel or suspend the contests (or any portion thereof) should virus, bugs, unauthorized human intervention, or other causes
                corrupt administration, security, fairness, integrity or proper operation of the contest (or any portion thereof) warrant doing so. Notification of such changes may be provided by
                eDoxa to its customers but will not be required.
              </p>
            </article>
            <article>
              <h3>INACTIVITY POLICY</h3>
              <p>
                For any account that has been dormant, eDoxa reserves the right to assess a maintenance fee of $2.00 per month from the remaining balance on deposit. A dormant account is any account
                in which the member has not logged on to the Site in three consecutive months. The inactivity fee will commence on the day after becoming dormant until the player&#39;s balance is
                depleted to zero.
              </p>
            </article>
            <article>
              <h3>WITHDRAWAL AND PAYMENT OF PRIZES</h3>
              <p>
                Entrants may withdraw their cash prize awards as well as cash deposits by using the “Withdrawal” option on the Website. Entrants may be requested to complete an affidavit of
                eligibility and a liability/publicity release (unless prohibited by law) and/or appropriate tax forms and forms of identification including but not limited to a Driver’s License, Proof
                of Residence, and/or any information relating to payment/deposit accounts as reasonably requested by eDoxa in order to complete the withdrawal of prizes. Failure to comply with this
                requirement may result in disqualification and forfeiture of any prizes. Disqualification or forfeiture of any prizes may also occur if it is determined any such entrant did not comply
                with these Terms of Use in any manner.
              </p>
              <p>Withdrawals maybe requested by any of the following methods:</p>
              <ul>
                <li>Direct Bank Transfers – Minimum amount: US $50. Countries available: US</li>
                <li>Wire Transfers – Minimum amount: US $200. Countries available: All</li>
                <li>
                  Wire transfers to international accounts will be initiated in USD and at the spot rate offered by the banks at the time. eDoxa derives no benefit from currency exchanges and will not
                  enter any debate about various exchange rates offered.
                </li>
                <li>Paypal – Minimum amount: US $20. Countries available: All</li>
                <li>
                  You may withdraw into the same PayPal account that you used to deposit money with. If you did not deposit money with a PayPal account or want to withdraw to a different account then
                  we may have to perform additional checks.
                </li>
                <li>Check – Minimum amount: US $200. Countries available: All</li>
              </ul>
              <p>
                Checks will be in USD and we do not charge any fees for issuing them. Although, we can’t be responsible for any charges made by banks or other financial institutions for banking or
                cashing checks. Furthermore, many of our checks are issued across borders, and some banks or financial institutions charge additional fees for handling international checks. Before
                requesting a check, we suggest that you check with your bank or financial institution for any charges that you may incur when banking or cashing your check.
              </p>
              <p>We highly recommend that you deposit your check at a reputable bank or financial institution, and avoid using a check cashing store.</p>
              <p>
                Checks within the US will be delivered by regular mail and are expected to arrive within 15 business days. International checks under $1,000 USD will be sent by regular mail, and are
                expected to arrive within 15 business days. International checks over $1,000 USD will be delivered by courier and are expected to arrive within 7 business days. To be able to use our
                courier service we will need your contact phone number and your address must not be a PO Box or a rural route address.
              </p>
              <p>
                Checks are sent to the full name and mailing address listed in your eDoxa account at the time you make your withdraw request. If your check is sent to an incorrect name or mailing
                address, you will incur a fee to place a stop-payment order on this check.
              </p>
              <p>
                We conduct anti-fraud checks on wagering patterns and deposits of users prior to processing all withdrawals. We may request additional information before your withdrawal is approved.
                We reserve the right to refuse any withdrawal request that doesn’t meet the guidelines of our Terms of Use.
              </p>
              <p>
                We may request that you provide your mailing address and SSN before the withdrawal is processed. This will help us in the event that your annual net winnings exceed $600 and we are
                required to file a 1099-MISC tax form.
              </p>
              <p>Promotional deposits, credits, and other bonuses may not be withdrawn from an eDoxa account unless appropriate terms of the promotion are achieved first by the user.</p>
              <p>
                All taxes associated with the receipt of any prize are the sole responsibility of the winner. In the event that the awarding of any prizes to winners of Contests is challenged by any
                legal authority, eDoxa reserves the right in its sole discretion to determine whether or not to award such prizes.
              </p>
              <p>
                No substitution or transfer of prize is permitted, except that eDoxa reserves the right to substitute a prize of equal value or greater if the advertised prize is unavailable. All
                prizes are awarded “as is” and without warranty of any kind, express or implied (including, without limitation, any implied warranty of merchantability for a particular purpose).
              </p>
              <p>Any withdrawal requests, after approved by eDoxa, will be credited back to the same credit card or method of payment used to deposit funds on the Website.</p>
              <p>
                eDoxa will only release withdrawals to a different credit card or other payment method other than that which was used to make deposit(s) after the aggregate amount of such deposit(s)
                has already been released back to the credit card(s) or payment method(s) used for the deposit(s).
              </p>
            </article>
            <article>
              <h3>SITE RULES OF CONDUCT</h3>
              <p>
                Chat, Message Board, and eMessage Policy eDoxa reserves the right to temporarily or permanently ban members who violate these rules of conduct, or who in any way abuse the community
                purpose of the chat areas. eDoxa reserves the right to remove any posts for any reason.
              </p>
              <ul>
                <li>Obscene, lewd, slanderous, pornographic, abusive, violent, insulting, indecent, threatening and harassing language of any kind will not be tolerated.</li>
                <li>Impersonating other members is not allowed.</li>
                <li>Opinions or comments on the subject at hand are welcomed, but attacking (flaming) individuals, companies or products is not allowed</li>
                <li>Advertising or promotion of other companies or URLs is not allowed.</li>
                <li>Do not share personal information (your name, phone number, home address, password) with others on the Site.</li>
                <li>No advertising of any kind is allowed in messages.</li>
                <li>No copyright materials are allowed in messages.</li>
                <li>eDoxa is not responsible for any information you choose to disclose to others.</li>
              </ul>
            </article>
            <article>
              <h3>ESPORTS CODE OF CONDUCT</h3>
              <p>
                By signing up for an account and/or participating in any game or tournament offered on eDoxa, you agree to refrain from engaging in any behavior causing or likely to cause harm to or
                reflect negatively upon the brand, reputation, or goodwill of eDoxa, including those of its affiliates, sponsors, and business partners. Members are expected to behave in a manner
                consistent with the eDoxa Esports Code of Conduct available at Esports Code of Conduct, which includes, but is not limited to, standards of honesty, respect, equality, and fair play.
                eDoxa reserves the right, in its sole and exclusive discretion, to monitor and evaluate any member’s past or present activity or online communications in assessing compliance with this
                term. In the event any member is found to be engaging, to have previously engaged, or is suspected to have engaged in behaviour that is contrary to the rules, standards, and valued
                expressed herein, eDoxa hereby reserves the right to suspend or terminate any member’s account, to block Site access, disqualify them from any tournament, void winnings, and in the
                event illegal activity is discovered, to disclose member information to law enforcement, and/or to commence legal action at the sole and exclusive discretion of eDoxa.
              </p>
            </article>
            <article>
              <h3>MEMBER FEEDBACK POLICY</h3>
              <p>
                While using the Services, and as a continuing condition of your use of eDoxa, you agree to comply with the Site&#39;s policy on feedback. Abuse of the Site&#39;s feedback system
                undermines the integrity of the feedback system and decreases trust within the community. eDoxa is not legally responsible for the remarks that members post, even if those remarks are
                defamatory. However, this law does not protect the person who leaves the feedback, therefore the member would be responsible for it. There are limited occasions when eDoxa will remove
                feedback comments.
              </p>
              <p>
                eDoxa will remove feedback ratings and comments if they meet the standard for feedback abuse or members mutually agree. Feedback that meets any of the circumstances below is feedback
                abuse and may be subject to removal:
              </p>
              <p>eDoxa is provided with a court order finding that the feedback is libelous, slanderous, defamatory or otherwise illegal.</p>
              <p>The feedback comment contains profane, vulgar, obscene, or racist language or adult material.</p>
              <p>The feedback comment contains personal identifying information about another member, including real name, address, phone number, e-mail address, etc..</p>
              <p>The feedback comment contains links or scripts.</p>
              <p>Feedback left by members who are indefinitely suspended for certain policy violations or are in violation of the T&amp;Cs.</p>
              <p>
                If members suspect that feedback left for them is in violation of the Site&#39;s feedback policy and constitutes abuse, they are encouraged to email info@edoxa.gg to report this
                violation. eDoxa will also offer the opportunity to members to withdraw feedback if both members mutually agree to the removal of a specified comment or feedback rating. Both members
                must email info@edoxa.gg.
              </p>
            </article>
            <article>
              <h3>ANTI-SPAM POLICY</h3>
              <p>
                eDoxa prohibits any activity commonly referred to as &quot;Spam&quot;. Members who are reported and whose claims of &quot;Spam&quot; are validated by eDoxa will have their respective
                accounts either immediately TERMINATED or SUSPENDED, at the sole discretion of eDoxa.
              </p>
              <p>Additionally, any winnings (if any) may be voided at the sole discretion of eDoxa. eDoxa defines &quot;Spam&quot; as:</p>
              <ul>
                <li>
                  Electronic mail messages addressed to a recipient with whom the initiator does not have an existing business or personal relationship or is not sent at the request of, or with the
                  express consent of, the recipient;
                </li>
                <li>
                  Messages posted to Usenet and message boards that are off-topic (unrelated to the topic of discussion), cross-posted to unrelated message boards or discussion threads, or posted in
                  excessive volume;
                </li>
                <li>
                  Solicitations posted to chat rooms, or to groups or individuals via Internet Relay Chat or &quot;Instant Messaging&quot; system (such as Skype);  eDoxa may undertake, at its sole
                  discretion and with or without prior notice, the following enforcement actions:
                </li>
                <li>
                  Account Suspension: Upon the receipt of a credible and validated complaint, eDoxa may also elect to immediately suspend the membership of the member implicated in the abuse.
                  Suspension serves as a &quot;Final&quot; warning and will prevent the member from continuing their abusive &quot;Spamming&quot; behavior. eDoxa will evaluate each validated abuse
                  incident on a case-by-case basis and impose Termination or Suspension at its sole discretion, and may void any associated winnings. The Site reserves the right to lift the suspension
                  of a member at any time, at its sole discretion.
                </li>
                <li>
                  Account Termination: Upon the receipt of a credible and validated complaint, the Site may immediately terminate the membership of the individual member implicated in the abuse and
                  may void any associated winnings.
                </li>
              </ul>
            </article>
            <article>
              <h3>ABUSE REPORTING</h3>
              <p>If you wish to report a violation of our Anti-Spam Policy, please forward all evidence of abuse to info@edoxa.gg Please refer responsibly.</p>
            </article>
            <article>
              <h3>TERMINATION AND EFFECT OF TERMINATION</h3>
              <p>
                In addition to any other legal or equitable remedy, eDoxa may, without prior notice, immediately revoke any or all of your rights granted hereunder. In such event, you will immediately
                cease all access to and use of the eDoxa Website. eDoxa may revoke any password(s) and/or account identification issued to you and deny you access to and use of the Website. Any such
                action shall not affect any rights and obligations arising prior thereto. All provisions of the Terms of Use which by their nature should survive termination shall survive termination,
                including, without limitation, ownership provisions, warranty disclaimers, indemnity and limitations of liability.
              </p>
            </article>
            <article>
              <h3>DISCLAIMER OF WARRANTIES</h3>
              <p>
                THE WEBSITE, INCLUDING, WITHOUT LIMITATION, ALL CONTENT, SOFTWARE, AND FUNCTIONS MADE AVAILABLE ON OR ACCESSED THROUGH OR SENT FROM THE WEBSITE, ARE PROVIDED “AS IS,” “AS AVAILABLE,”
                AND “WITH ALL FAULTS.” TO THE FULLEST EXTENT PERMISSIBLE BY LAW, THE COMPANY AND ITS PARENTS, SUBSIDIARIES AND AFFILIATES MAKE NO REPRESENTATION OR WARRANTIES OR ENDORSEMENTS OF ANY
                KIND WHATSOEVER (EXPRESS OR IMPLIED) ABOUT: (A) THE WEBSITE; (B) THE CONTENT AND SOFTWARE ON AND PROVIDED THROUGH THE WEBSITE; (C) THE FUNCTIONS MADE ACCESSIBLE ON OR ACCESSED THROUGH
                THE WEBSITE; (D) THE MESSAGES AND INFORMATION SENT FROM THE WEBSITE BY USERS; (E) ANY PRODUCTS OR SERVICES OFFERED VIA THE WEBSITE OR HYPERTEXT LINKS TO THIRD PARTIES; AND/OR (F)
                SECURITY ASSOCIATED WITH THE TRANSMISSION OF SENSITIVE INFORMATION THROUGH THE WEBSITE OR ANY LINKED SITE. THE COMPANY DOES NOT WARRANT THAT THE WEBSITE, ANY OF THE WEBSITES’ FUNCTIONS
                OR ANY CONTENT CONTAINED THEREIN WILL BE UNINTERRUPTED OR ERROR-FREE; THAT DEFECTS WILL BE CORRECTED; OR THAT THE WEBSITES OR THE SERVERS THAT MAKES THEM AVAILABLE ARE FREE OF VIRUSES
                OR OTHER HARMFUL COMPONENTS.
              </p>
              <p>
                THE COMPANY DOES NOT WARRANT THAT YOUR ACTIVITIES OR USE OF THE WEBSITE IS LAWFUL IN ANY PARTICULAR JURISDICTION AND, IN ANY EVENT, THE COMPANY SPECIFICALLY DISCLAIMS SUCH WARRANTIES.
                YOU UNDERSTAND THAT BY USING ANY OF THE FEATURES OF THE WEBSITE, YOU ACT AT YOUR OWN RISK, AND YOU REPRESENT AND WARRANT THAT YOUR ACTIVITIES ARE LAWFUL IN EVERY JURISDICTION WHERE YOU
                ACCESS OR USE THE WEBSITE OR THE CONTENT. FURTHER, THE COMPANY AND ITS PARENTS, SUBSIDIARIES AND AFFILIATES DISCLAIM ANY EXPRESS OR IMPLIED WARRANTIES INCLUDING, WITHOUT LIMITATION,
                NONINFRINGEMENT, MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE, AND TITLE.
              </p>
              <p>
                THE COMPANY, ITS PARENTS, SUBSIDIARIES AND AFFILIATES, AND THE DIRECTORS, OFFICERS, EMPLOYEES, AND OTHER REPRESENTATIVES OF EACH OF THEM, SHALL NOT BE LIABLE FOR THE USE OF THE WEBSITE
                INCLUDING, WITHOUT LIMITATION, THE CONTENT AND ANY ERRORS CONTAINED THEREIN. SOME JURISDICTIONS LIMIT OR DO NOT ALLOW THE DISCLAIMER OF IMPLIED OR OTHER WARRANTIES SO THE ABOVE
                DISCLAIMER MAY NOT APPLY TO THE EXTENT SUCH JURISDICTION’S LAW IS APPLICABLE TO THIS AGREEMENT.
              </p>
            </article>
            <article>
              <h3>LIMITATION OF LIABILITY</h3>
              <p>
                YOU UNDERSTAND AND AGREE THAT THE COMPANY LIMITS ITS LIABILITY IN CONNECTION WITH YOUR USE OF THE WEBSITE AS SET FORTH BELOW: UNDER NO CIRCUMSTANCES SHALL THE COMPANY, ITS PARENTS,
                SUBSIDIARIES, OR AFFILIATES, OR THE DIRECTORS, OFFICERS, EMPLOYEES, OR OTHER REPRESENTATIVES OF EACH OF THEM (COLLECTIVELY, THE “COMPANY ENTITIES AND INDIVIDUALS”), BE LIABLE TO YOU
                FOR ANY LOSS OR DAMAGES OF ANY KIND (INCLUDING, WITHOUT LIMITATION, FOR ANY SPECIAL, DIRECT, INDIRECT, INCIDENTAL, EXEMPLARY, ECONOMIC, PUNITIVE, OR CONSEQUENTIAL DAMAGES) THAT ARE
                DIRECTLY OR INDIRECTLY RELATED TO (1) THE WEBSITE, THE CONTENT, OR YOUR UPLOAD INFORMATION; (2) THE USE OF, INABILITY TO USE, OR PERFORMANCE OF THE WEBSITE; (3) ANY ACTION TAKEN IN
                CONNECTION WITH AN INVESTIGATION BY THE COMPANY OR LAW ENFORCEMENT AUTHORITIES REGARDING YOUR USE OF THE WEBSITE OR CONTENT;(4) ANY ACTION TAKEN IN CONNECTION WITH COPYRIGHT OWNERS; OR
                (5) ANY ERRORS OR OMISSIONS IN THE WEBSITE’S TECHNICAL OPERATION, EVEN IF FORESEEABLE OR EVEN IF THE COMPANY ENTITIES AND INDIVIDUALS HAVE BEEN ADVISED OF THE POSSIBILITY OF SUCH
                DAMAGES WHETHER IN AN ACTION OF CONTRACT, NEGLIGENCE, STRICT LIABILITY TORT (INCLUDING, WITHOUT LIMITATION, WHETHER CAUSED IN WHOLE OR IN PART BY NEGLIGENCE, ACTS OF GOD,
                TELECOMMUNICATIONS FAILURE, OR THEFT OR DESTRUCTION OF THE WEBSITE). IN NO EVENT WILL THE COMPANY ENTITIES AND INDIVIDUALS BE LIABLE TO YOU OR ANYONE ELSE FOR LOSS OR INJURY,
                INCLUDING, WITHOUT LIMITATION, DEATH OR PERSONAL INJURY. SOME STATES DO NOT ALLOW THE EXCLUSION OR LIMITATION OF INCIDENTAL OR CONSEQUENTIAL DAMAGES, SO THE ABOVE LIMITATION OR
                EXCLUSION MAY NOT APPLY TO YOU. IN NO EVENT SHALL THE COMPANY ENTITIES AND INDIVIDUALS TOTAL LIABILITY TO YOU FOR ALL DAMAGES, LOSSES, OR CAUSES OF ACTION EXCEED ONE HUNDRED DOLLARS
                ($100). THE COMPANY ENTITIES AND INDIVIDUALS ARE NOT RESPONSIBLE FOR ANY DAMAGE TO ANY USER’S COMPUTER, HARDWARE, COMPUTER SOFTWARE, OR OTHER EQUIPMENT OR TECHNOLOGY INCLUDING, WITHOUT
                LIMITATION, DAMAGE FROM ANY SECURITY BREACH OR FROM ANY VIRUS, BUGS, TAMPERING, FRAUD, ERROR, OMISSION, INTERRUPTION, DEFECT, DELAY IN OPERATION OR TRANSMISSION, COMPUTER LINE OR
                NETWORK FAILURE OR ANY OTHER TECHNICAL OR OTHER MALFUNCTION. YOUR ACCESS TO AND USE OF THIS WEBSITE IS AT YOUR RISK. IF YOU ARE DISSATISFIED WITH THE WEBSITE OR ANY OF THE CONTENT,
                YOUR SOLE AND EXCLUSIVE REMEDY IS TO DISCONTINUE ACCESSING AND USING THE WEBSITE OR THE CONTENT. YOU RECOGNIZE AND CONFIRM THAT IN THE EVENT YOU INCUR ANY DAMAGES, LOSSES OR INJURIES
                THAT ARISE OUT OF THE COMPANY’S ACTS OR OMISSIONS, THE DAMAGES, IF ANY, CAUSED TO YOU ARE NOT IRREPARABLE OR SUFFICIENT TO ENTITLE YOU TO AN INJUNCTION PREVENTING ANY EXPLOITATION OF
                ANY WEBSITE OR OTHER PROPERTY OWNED OR CONTROLLED BY THE COMPANY AND/OR ITS PARENTS, SUBSIDIARIES, AND/OR AFFILIATES OR YOUR UPLOAD INFORMATION, AND YOU WILL HAVE NO RIGHTS TO ENJOIN
                OR RESTRAIN THE DEVELOPMENT, PRODUCTION, DISTRIBUTION, ADVERTISING, EXHIBITION OR EXPLOITATION OF ANY COMPANY WEBSITE OR OTHER PROPERTY OR YOUR UPLOAD INFORMATION OR ANY AND ALL
                ACTIVITIES OR ACTIONS RELATED THERETO. BY ACCESSING THE WEBSITE, YOU UNDERSTAND THAT YOU MAY BE WAIVING RIGHTS WITH RESPECT TO CLAIMS THAT ARE AT THIS TIME UNKNOWN OR UNSUSPECTED.
                ACCORDINGLY, YOU AGREE TO WAIVE THE BENEFIT OF ANY LAW, INCLUDING, TO THE EXTENT APPLICABLE, CALIFORNIA CIVIL CODE SECTION 1542, THAT OTHERWISE MIGHT LIMIT YOUR WAIVER OF SUCH CLAIMS.
              </p>
            </article>
            <article>
              <h3>INTELLECTUAL PROPERTY RIGHTS</h3>
              <p>
                The content on the Website, including without limitation, the text, software, scripts, graphics, photos, sounds, music, videos, interactive features and the like and the trademarks,
                service marks and logos contained therein (the “Intellectual Property”), are owned by or licensed to eDoxa, subject to copyright and other intellectual property rights under United
                States and foreign laws and international conventions. Content on the Website is provided to you AS IS for your information and personal use only and may not be used, copied,
                reproduced, distributed, transmitted, broadcast, displayed, sold, licensed, or otherwise exploited for any other purposes whatsoever without the prior written consent of the respective
                owners. eDoxa reserves all rights not expressly granted in and to the Website and the Intellectual Property. You agree to not engage in the use, copying, or distribution of any of the
                Intellectual Property other than expressly permitted herein. If you download or print a copy of the Intellectual Property for personal use, you must retain all copyright and other
                proprietary notices contained therein. You agree not to circumvent, disable or otherwise interfere with security related features of the Website or features that prevent or restrict
                use or copying of any Intellectual Property or enforce limitations on use of the Website or the Intellectual Property therein.
              </p>
              <p>
                Some of the Services may allow you to submit or transmit audio, video, text, or other materials (collectively, “User Submissions”) to or through the Services. When you provide User
                Submissions, you grant to eDoxa, its parents, subsidiaries, affiliates, and partners a non-exclusive, worldwide, royalty-free, fully sublicenseable license to use, distribute, edit,
                display, archive, publish, sublicense, perform, reproduce, make available, transmit, broadcast, sell, translate, and create derivative works of those User Submissions, and your name,
                voice, likeness and other identifying information where part of a User Submission, in any form, media, software, or technology of any kind now known or developed in the future,
                including, without limitation, for developing, manufacturing, and marketing products. You hereby waive any moral rights you may have in your User Submissions.
              </p>
              <p>
                In addition, you agree that any User Submissions you submit shall not contain any material that is, in the sole and absolute discretion of eDoxa, inappropriate, obscene, vulgar,
                unlawful, or otherwise objectionable (hereinafter, “Prohibited Content”). Posting of any Prohibited Content, in addition to any and all other rights and remedies available to eDoxa,
                may result in account suspension or termination.
              </p>
              <p>
                We respect your ownership of User Submissions. If you owned a User Submission before providing it to us, you will continue owning it after providing it to us, subject to any rights
                granted in the Terms of Use and any access granted to others. If you delete a User Submission from the Services, our general license to that User Submission will end after a reasonable
                period of time required for the deletion to take full effect. However, the User Submission may still exist in our backup copies, which are not publicly available. If your User
                Submission is shared with third parties, those third parties may have retained copies of your User Submissions. In addition, if we made use of your User Submission before you deleted
                it, we will continue to have the right to make, duplicate, redistribute, and sublicense those pre-existing uses, even after you delete the User Submission. Terminating your account on
                a Service will not automatically delete your User Submissions.
              </p>
              <p>
                We may refuse or remove a User Submission without notice to you. However, we have no obligation to monitor User Submissions, and you agree that neither we nor our parents,
                subsidiaries, affiliates, employees, or agents will be liable for User Submissions or any loss or damage resulting from User Submissions.
              </p>
              <p>
                Except as provided in the Privacy Policy, we do not guarantee that User Submissions will be private, even if the User Submission is in a password- protected area. Accordingly, you
                should not provide User Submissions that you want protected from others.
              </p>
              <p>
                You represent and warrant that you have all rights necessary to grant to eDoxa the license above and that none of your User Submissions are defamatory, violate any rights of third
                parties (including intellectual property rights or rights of publicity or privacy), or violate applicable law.
              </p>
            </article>
            <article>
              <h3>GOVERNING LAW AND DISPUTES</h3>
              <p>
                These Terms and Conditions will be governed by and construed in accordance with the laws of the Province of Quebec and the federal laws of Canada applicable therein, without regard to
                its conflicts of law provisions, and you hereby consent to the exclusive jurisdiction of and venue in the courts of the Province of Quebec located in Montreal, Quebec, Canada, in all
                disputes arising out of or relating to the Services. Notwithstanding any other provisions of these T&amp;Cs, we may seek injunctive or other equitable relief from any court of
                competent jurisdiction. You agree that any dispute that cannot be resolved between the parties shall be resolved individually, without resort to any form of class action.
              </p>
              <p>
                We make no representation that this Site is operated in accordance with the laws or regulations of, or governed by, other nations. By utilizing the Services and participating in Site
                activities, you certify that you meet the age and other eligibility requirements for the Site and the Services set forth in the T&amp;Cs. If you do not meet the age and other
                eligibility requirements, please discontinue using the Site and the Services immediately.
              </p>
            </article>
          </section>
        </CardBody>
      </Card>
    </Container>
  );
};

const enhance = compose<any, any>(withLocalize);

export default enhance(TermsOfServices);
