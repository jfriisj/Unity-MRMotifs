Digital Object Identifier no. 10.1109/TVCG.2025.3616760
IEEE TRANSACTIONS ON VISUALIZATION AND COMPUTER GRAPHICS, VOL. 31, NO. 11, NOVEMBER 2025
10109
Received 11 April 2025; revised 18 June 2025; accepted 22 July 2025.  
Date of publication 2 October 2025; date of current version 3 November 2025.
This article has supplementary downloadable material available at  
https://doi.org/10.1109/TVCG.2025.3616760, provided by the authors.
1077-2626 © 2025 IEEE. All rights reserved, including rights for text and data mining, and training of artificial intelligence and similar technologies.  
Personal use is permitted, but republication/redistribution requires IEEE permission. See https://www.ieee.org/publications/rights/index.html for more information.
Investigating the Effects of Haptic Illusions in Collaborative Virtual
Reality
Yannick Weiss
, Julian Rasch
Jonas Fischer
, and Florian Müller
Fig. 1: In this work, we investigate how visual shape and size illusions influence users’ performance, experience, and behavior during a
collaborative task in VR. A illustrates the task setup from our user study, where participants were asked to hand over a physical object
while we independently manipulated its visual shape or size in VR for each participant. B presents the different visual stimuli used
across trials. In each trial, either the shape or the size of the object was altered independently for the two participants. C depicts the
physical setup (top) and its corresponding virtual environment (bottom). The setup included two head-mounted displays equipped with
hand tracking and a physically tracked prop. In VR, participants saw virtual representations of their partner’s head and hands, along
with the manipulated object.
Abstract— Our sense of touch plays a crucial role in physical collaboration, yet rendering realistic haptic feedback in collaborative
extended reality (XR) remains a challenge. Co-located XR systems predominantly rely on prefabricated passive props that provide
high-fidelity interaction but offer limited adaptability. Haptic Illusions (HIs), which leverage multisensory integration, have proven
effective in expanding haptic experiences in single-user contexts. However, their role in XR collaboration has not been explored. To
examine the applicability of HIs in multi-user scenarios, we conducted an experimental user study (N=30) investigating their effect on a
collaborative object handover task in virtual reality. We manipulated visual shape and size individually and analyzed their impact on
users’ performance, experience, and behavior. Results show that while participants adapted to the illusions by shifting sensory reliance
and employing specific sensorimotor strategies, visuo-haptic mismatches reduced both performance and experience. Moreover,
mismatched visualizations in asymmetric user roles negatively impacted performance. Drawing from these findings, we provide
practical guidelines for incorporating HIs into collaborative XR, marking a first step toward richer haptic interactions in shared virtual
spaces.
Index Terms—haptic illusions, multi-user collaboration, virtual and extended reality
1
INTRODUCTION
From shaking hands to handing someone a tool to lifting bulky furniture;
our sense of touch is crucial in everyday interpersonal communication
• Yannick Weiss, Julian Rasch, and Jonas Fischer are with LMU Munich.
E-mail: yannick.weiss@ifi.lmu.de, julian.rasch@ifi.lmu.de,
jonas.fischer@campus.lmu.de
• Florian Müller is with TU Darmstadt. E-mail:
florian.mueller@tu-darmstadt.de
and collaboration. However, when working together in extended reality
(XR) environments, this haptic aspect of collaboration is lost, as XR
systems primarily rely on simple vibration feedback from hand-held
devices, which cannot reproduce the wide range of haptic feedback we
experience in real-world interactions [28]. While research has brought
forth many approaches to enrich haptic feedback in XR – including
grounded encounter-type systems [34,35] as well as hand-held [9,13]
or wearable [40] devices – these systems are generally either limited
to produce haptic feedback for one user at a time or require both
users to wear or hold the device constantly, severely restricting the
possible collaborative interaction between them. Consequently, most
approaches to delivering realistic haptic experiences in collaborative
settings rely on integrating physical proxy objects that can be picked up,
placed, or handed over among users [12,25]. While these passive props
offer high-fidelity information regarding the specific geometric and
Authorized licensed use limited to: University of Southern Denmark. Downloaded on December 01,2025 at 08:22:43 UTC from IEEE Xplore.  Restrictions apply. 
IEEE TRANSACTIONS ON VISUALIZATION AND COMPUTER GRAPHICS, VOL. 31, NO. 11, NOVEMBER 2025
10110
material features they were designed to represent, they cannot adapt
these properties, which severely limits the range of haptic experiences
they can provide.
Haptic Illusions (HIs) have been shown to enrich and extend the spec-
trum of haptic feedback which can be delivered by passive props [30].
Sensory illusions, including HIs, leverage the interconnected relations
of sensory modalities in creating our unified perception [19], which
enables one sense, such as vision or audition, to influence and partially
overwrite another, such as our haptic sense. This multisensory integra-
tion can be exploited by deliberately manipulating presented visual or
auditory cues during interactions. Previous research has shown these
manipulations to be effective in altering the perceived geometric fea-
tures – e.g., shape [48] and size [3] – and material properties – e.g.,
temperature [24], stiffness [56], weight [43], and surface texture [20]
– of physical objects. They achieve this by manipulating the visual
or auditory presentation of the objects [3,20,24,48] or their behavior
during interactions such as pressing [56] or lifting [43].
However, prior XR research solely focused on the effects of HIs
on single users, overlooking their possible influence on collaborative
settings, where users need to interact with one another and physical
props may need to be shared. In these settings, HIs can enable the
same physical props to represent different digital objects for each user,
leading to divergent perceptions of properties such as size or shape. In
addition to extending the range and adaptability of haptic feedback,
this also enables novel opportunities for individual personalization of
haptic experiences for each user, for instance, to help balance collab-
orative trainings [1,45] and competitive games [23,29,39] based on
competency levels, or increase accessibility through personal haptic
calibration. However, when users attempt to share and hand over these
manipulated props, these mismatches may cause coordination errors,
disrupt interaction flow, or result in physical discomfort.
To address these potential challenges, we investigate the impact
of HIs on multi-user collaboration in co-located XR environments.
Specifically, this work investigates how visual manipulation of objects’
shapes and sizes affects users’ experience, performance, and behavior
in a collaborative handover task in Virtual Reality (VR). For this, we
conducted an experimental user study (N=30) with pairs of participants
who were tasked to hand over a physical object between them while we
varied the visual shape or size in their individual representations. Using
a mixed-methods approach, we found that visuo-haptic mismatches
consistently degrade both task performance and user experience, even
when users actively adapt by shifting sensory reliance and employing
mitigation strategies. Moreover, mismatched visualizations between
users can have an even greater impact on performance than visuo-haptic
discrepancies alone, with the extent of this effect varying depending
on user’s role in the interaction. Derived from our quantitative and
qualitative findings, we propose guidelines for integrating HIs into
collaborative XR contexts. With this work, we provide a foundational
step toward enabling richer, more seamless haptic experiences in col-
laborative XR facilitated through HIs, paving the way for more natural
and effective collaboration in shared virtual environments.
2
RELATED WORK
This work draws from a large body of research relating to haptics
and collaboration in XR. In the following, we provide an overview of
conventional haptic rendering techniques (subsection 2.1), their use
in collaborative XR systems (2.2), and the potential of HIs in these
contexts (2.3).
2.1
Active Haptic Rendering
Producing realistic haptic feedback is one of the key challenges in cur-
rent immersive systems [52]. Haptic perception is complex, comprising
both kinesthetic as well as cutaneous cues, such as pressure, vibration,
and thermal sensations [27]. While current state-of-the-art devices rely
on vibrotactile feedback in hand-held controllers to offer basic feedback,
these systems do not suffice to fully reproduce the intricacies of haptic
experiences we are presented with when interacting with our natural sur-
roundings [27,28]. To address this, research has developed numerous
approaches to deliver more realistic haptic sensations to single users.
High-resolution robotic devices – such as the PHANToM [35], Omega1,
or Falcon [34] – produce precise forces at high-frequencies while the
user is holding their end-effector. Encounter-type haptic systems [36]
trade this precision and speed to eliminate the requirement for constant
contact and an increased working area. However, both approaches
require bulky systems, which can only generate haptic sensations for
a single user at a time. Hand-held devices [9,13] and wearables [40]
offer more mobility and may be distributed to several users. However,
these systems still obstruct or constrict the users’ hands. This confines
interaction and feedback to the single device, limiting its suitability
for collaborative settings where users may need to interact with shared
objects or each other.
Overall, while conventional haptic rendering techniques have proven
effective for specific single-user tasks, they do not easily translate to
multi-user settings, necessitating alternative approaches for collabora-
tive XR.
2.2
Passive Haptics in Collaborative XR
Collaborative XR systems are becoming more prevalent and mature,
particularly with the advancement in augmented and mixed reality
enabling novel opportunities for co-located cooperation [17]. While
conventional haptic approaches are used to support remote coopera-
tion [51], they are largely inadequate for co-located collaboration. So-
cial games and Computer Supported Cooperative Work (CSCW) in XR
consequently predominantly either provide no haptic feedback or rely
on physical props placed in the environment. These comprise hand-held
passive props for each user – such as sports rackets and sticks [29,39]
or swords to safely hit each other [23] – as well as prefabricated objects
spread on a table [25,42] or around the environment [12,16], with which
all users may physically interact. Single-user studies showed passive
physical props to improve task performance [57], presence [26,57], and
spatial orientation [26]. To extend the perceived interaction space while
still allowing passive props to be touched, some approaches employ
redirected walking techniques on multiple users [4,15,16,37]. With
subtle manipulations (e.g., rotations) of the virtual scene, these manipu-
late users’ walking trajectories, which is used to avoid collisions [15]
while still allowing for users to interact with physical objects [16] or
each other [37].
The integration of passive props offers realistic shared haptic experi-
ences with the potential to represent predefined geometric and material
characteristics with high fidelity. However, they are not able to dynami-
cally adjust their properties, severely limiting their versatility regarding
haptic feedback as well as potential applications. HIs offer a potential
approach to extend the range of haptic feedback these physical props
can offer without requiring additional complex hardware.
2.3
Haptic Illusions
When exploring our physical environment, we naturally rely on multi-
ple senses, leading to crossmodal interactions that shape our perception.
Sensory illusions, such as HIs, emerge when conflicting information
is presented across different sensory modalities, shifting subjective
perception toward a unified interpretation of the stimuli [19]. This
integration can be exploited to generate altered haptic sensations by de-
liberately inducing mismatches in presentation, e.g., through modifying
visual or auditory cues during object interactions. Prior research has
shown these approaches to efficaciously alter the perceived geometry
– e.g., shape [48] and size [3,48] – and material properties – such as
stiffness [56], weight [43], temperature [24], and surface texture [20]
– of interacted objects (see [30] for a comprehensive overview). Fo-
cusing on illusions targeting shape or size manipulations, numerous
works have shown that physical proxy objects, coupled with discrepant
visual representations in Augmented Reality (AR) or VR, can suffice
to render the sensation of a large variety of virtual object shapes and
sizes [31,46,47]. In a psychophysical experiment, Tinguy et al. [48] in-
vestigated the degree to which visual and haptic objects may mismatch
in VR without users being able to detect it. They showed that changes
1https://www.forcedimension.com/products/omega,
accessed:
2025-03-25
Authorized licensed use limited to: University of Southern Denmark. Downloaded on December 01,2025 at 08:22:43 UTC from IEEE Xplore.  Restrictions apply. 
10111
weiss ET AL.: INVESTIGATING THE EFFECTS OF HAPTIC ILLUSIONS IN COLLABORATIVE VIRTUAL REALITY
in object widths below 5.75% remain imperceptible, while changes in
the surface’s angle and curvature may change up to 43.8% and 66.66%,
respectively, before being detected. Further, Auda et al. [3] showed that
offsetting hand representations in VR while grabbing physical objects
can extend the effective range of allowed size mismatches. Analo-
gously, Ban et al. [6, 7] showed that visually distorting hand poses
during grasping allows for alterations of objects’ perceived shape.
While the large body of prior research into HIs shows the potential
benefits of integrating these illusions with passive tangible props to
enrich the haptic experience, they solely investigated HIs impact in
single-user scenarios. The benefits and drawbacks of HIs for collabora-
tive settings, which rely on passive props the most, have been scarcely
considered: In an analog setting, Lefebvre et al. [33] applied HIs on
a tabletop display, which allows anyone currently interacting with the
table to feel a deformation effect. For collaborative interactions, Arge-
laguet et al. [2] showed the potential of visually discriminating the
stiffness of a virtual object in tandem while both users interact on
separate tablet touchscreens.
Yet, the use of HIs for collaborative XR – where passive haptics
are the most prevalent – remains unexplored. To address this gap, we
investigate the impact of HIs in a multi-user collaborative VR task. By
exploring the challenges of integrating HIs into multi-user contexts, we
offer initial insights toward enabling richer and more dynamic haptic
experiences in collaborative XR to support user communication and
interaction.
3
STUDY DESIGN
We conducted an experimental user study to explore how HIs affect
user experience, performance, and behavior in multi-user collabora-
tion. Specifically, we examined how visuo-haptic illusions that alter
an object’s shape and size influence a handover task in VR. We used a
repeated-measures design, varying the virtual shape or size of the object
for each participant individually during the handover task, as presented
in Figure 1A. We decided to implement drastic changes rather than
gradual differences to ensure a clear perception of the manipulations
and to explore how significant mismatches between the real and virtual
worlds affect the handover process. With this, we aim to address the
research questions: (RQ1) How does the introduction of individual
visuo-haptic mismatches of an object’s shape and size influence per-
formance and user experience in a collaborative handover task in VR?
(RQ2) How do users adapt to these discrepancies in visuo-haptic and
interpersonal presentation?
3.1
Task & Stimuli
We paired participants in teams of two. In each trial, one person picked
up a physical object from a platform on their side of the room and
handed it to their partner, who then placed it onto another platform.
After they completed the placement and returned to their starting po-
sitions, participants were prompted to answer a short questionnaire
displayed in the virtual environment regarding their subjective experi-
ence. Afterward, the next trial began; this time, the opposite participant
was grabbing and handing over the physical object. There was no time
limit for the handover.
In each trial, we varied the visualization of the object for the targeted
participant (VIS), the visualization of the object for the opposing par-
ticipant (OTHERSVIS), and the role of the participant in the handover
(ROLE):
VIS: Depending on the trial, we visually varied either the shape
or the size of the virtual object. An overview of the changes
introduced is presented in Figure 1B. For the SHAPE-CONDITION
we decided to use the basic geometric primitives that differed in
local orientation and curvature (c.f. [48]) and displayed either a
(1) Cube, (2) Sphere, or (3) Pyramid. The virtual cube matched
the dimensions of the physical one exactly. For the others, we
respectively fitted the diameter of the sphere and the base of the
pyramid to the physical cube.
For the SIZE-CONDITION we displayed a cube of different sizes.
Previous work showed that props can effectively represent vir-
Fig. 2: Visualization of a single handover trial (left) and all combinations
of VIS, OTHERSVIS, and ROLE that we investigate (right).
tual objects up to 50% larger than their actual size [3]. Based
on this, we chose the following sizes for our experiment: (1)
Matching the physical cube (15cm × 15cm × 15cm), (2) Bigger
(×1.5 = 22.5cm), or (3) Smaller (÷1.5 = 10cm).
OTHERSVIS: Concurrent to varying the visualization of the respec-
tive participant, we also always varied the virtual object for the
opposing participant. We use the same possible shapes or sizes as
in VIS. We did not mix shape and size manipulations and only
varied either the shape or the size for both participants, depending
on the trial.
ROLE: The participant’s role in the handover could either be the
GIVER or RECEIVER of the object. The roles were switched after
each trial.
The combination of all variables resulted in 18 conditions for the
SHAPE-CONDITION (3 SHAPE× 3 OTHERSSHAPE× 2 ROLE) and
18 conditions for the SIZE-CONDITION (3 SIZE× 3 OTHERSSIZE×
2 ROLE). As both conditions share two situations where the visual
and physical objects match completely (once as GIVER & once as
RECEIVER), we removed these duplicate conditions. This resulted in a
total of 34 trials for each study session, which are displayed in Figure 2.
We randomized the trial order for each session.
3.2
Measurements
For each trial, we recorded the task completion time of the handover.
To make sure we do not discard any possible effects of visualization on
approach behavior, we measured the time from task start (participants in
center position) to task end (participants return to center post-handover).
Further, we collected subjective ratings from each participant on six
task-related statements (see subsection 4.2), using a 7-point Likert scale
(1 = strongly disagree, 7 = strongly agree). While collaborative VR
experiences are influenced by various broad factors such as presence,
involvement, and social connectedness, these are often shaped by com-
plex interpersonal dynamics beyond the scope of our manipulation.
Instead, we focused on aspects directly related to the interaction, task
performance, and visuo-haptic presentation. Additionally, we audio-
recorded the handover process to investigate communication strategies.
For additional qualitative insights, we allowed participants to freely
make comments at the end of each trial and conducted a semi-structured
interview at the end of the study.
3.3
Apparatus
The physical and virtual setup for the study is displayed in Figure 1C.
For participants to freely move around, we set up our experiment in
an area of ∼4.1m×1.9m. We placed two cardboard boxes in diago-
nal corners of this area (back-left and front-right corners) to serve as
platforms. They were 50cm×50cm wide and 102cm high. We marked
participants’ starting points in the center of our area, spaced 108cm
apart along the long axis. We created a virtual environment using
Authorized licensed use limited to: University of Southern Denmark. Downloaded on December 01,2025 at 08:22:43 UTC from IEEE Xplore.  Restrictions apply. 
IEEE TRANSACTIONS ON VISUALIZATION AND COMPUTER GRAPHICS, VOL. 31, NO. 11, NOVEMBER 2025
10112
Unity3D2 and displayed it in two HTC Vive Pro 23 Head-Mounted Dis-
plays (HMDs) sharing the same tracking space. The applications ran on
two desktop PCs4. To track and display participants’ hand movements,
we mounted an Ultraleap Leap Motion Controller 25 on each HMD. For
participants to see each other in the virtual environment, we integrated
network synchronization using Photon Fusion 26. We re-calibrated the
position of both HMDs before every session. Additionally, we attached
VIVE Trackers7 to the left arms of participants tracked by the HMD
of the respective other for increased precision in determining hand
positions. For the handover task, we built a 15cm×15cm×15cm cube
out of transparent acrylic glass and connected two VIVE trackers to the
top in opposing corners. The entire passive prop, including trackers,
weighed 1048 grams.
The virtual scene consisted of a minimal walled room with the di-
mensions of the physical interaction space. The participants’ hands
were represented as transparent outlines provided by Ultraleap. Be-
cause the generated changes in shape or size would cause the hands
of participants to clip into or hover above the virtual object when han-
dling the physical object, we employed hand redirection (cf. [5,58]) to
mitigate these mismatches. When participants reached out to grab the
object from a platform or the other person, we broke the one-to-one
mapping of real to virtual hand movements and subtly displaced the
visual hand models. We used linear interpolation to adjust the virtual
hands, ensuring they touched the virtual object at the same time and
place as the real hands touched the physical object. The opposing
participant saw the same adjusted hand representations. Additionally,
they were presented with a virtual avatar consisting only of spheres
for the head and eyes, thus only giving information about the other’s
head rotation in addition to their hand movements. The platforms were
virtually displayed with the same dimensions, with small indicators
on top showing where to place the physical object. We displayed our
questionnaire directly in the virtual environment, where participants
could interact with them using their free hands.
3.4
Procedure
After welcoming the participants, we informed them about the task,
the study’s objective, and our data processing procedure. We then
asked them to sign a consent form. Each participant then filled out a
questionnaire regarding their demographics, previous experience with
VR, and familiarity with the other participant in their joined session.
Afterward, we helped them put on the VR headsets and let them run
through a short training phase consisting of three complete trials of the
handover task and the subsequent survey. These three trials involved
handling (1) a matching object, (2) an object of a different size, and
(3) an object of a different shape. After ensuring that participants
understood their task, we started the experimental trials. Upon finishing
all trials, we helped participants remove their headsets. We then invited
both participants to a voluntary semi-structured interview where we
asked them to reflect and comment on their experiences, which was
audio-recorded and transcribed.
3.5
Participants
We recruited 30 participants through university mailing lists. 17 par-
ticipants described themselves as female and 13 as male. Participants’
age ranged from 18 to 71 (M = 26.70,SD = 8.94). 29 were right-
handed, and one was left-handed. 23 participants had experienced
VR before (8 below 2h, 12 between 2h and 20h, and 3 for more than
2Version 2022.3.3, https://unity.com/, accessed: 2025-02-25
3https://www.vive.com/us/product/vive-pro2/overview/,
accessed: 2025-03-10
4PC1: Ryzen 9 7900X3D processor, 64GB RAM, NVIDIA RTX 4080 Super
graphics card; PC2: Intel Core i7-6700K processor, 16GB RAM, NVIDIA
GeForce GTX 1080 graphics card
5https://leap2.ultraleap.com/products/
leap-motion-controller-2/, accessed: 2025-02-26
6https://doc.photonengine.com/fusion/v2/, accessed: 2025-03-10
7https://www.vive.com/us/support/wireless-tracker/
category_howto/about-vive-tracker.html, accessed: 2025-03-10
20h). Participants had normal or corrected-to-normal vision8 and no
known conditions affecting the haptic acuity of their hands. Participants
took part in the study as pairs of two. Two of the 15 pairs described
themselves as friends, while the remaining had not met before. We
offered participants 15C or university course credit as compensation.
Our institution’s ethics board approved this study.
3.6
Data Analysis
To analyze participants’ task completion times (TCT), we fitted Gener-
alized Linear Mixed Models (GLMMs) with a Gamma distribution and
a inverse link function using the Laplace approximation implemented
in the lme4 R-package [8]. Because completion times are the same for
both participants of a session, we analyzed the data on a session rather
than participant basis. For the SHAPE-CONDITION, we included the
SHAPE of the GIVER, SHAPE of the RECEIVER, and their interactions
as fixed effects. For the SIZE-CONDITION, we included the SIZE of
the GIVER and RECEIVER, and their interaction as fixed effects. For
both SHAPE-CONDITION and SIZE-CONDITION, we account for vari-
ability between participant groups by adding the unique session IDs
as a random effect. Additionally, we account for learning effects by
adding the trial index as another random effect.
For participants’ ordinal ratings (of our subjective statements 1-6),
we used Cumulative Link Mixed Models (CLMMs) with logit link
functions. The models were fitted with the adaptive Gauss-Hermite
quadrature approximation with 10 quadrature points, implemented in
the ordinal R-package [14]. For the SHAPE-Condition, we included
SHAPE, ROLE, OTHERSSHAPE, and their interactions as fixed effects.
For the SIZE-Condition, we used SIZE, ROLE, OTHERSSIZE, and their
interactions. For both, we added the participants’ unique IDs as a
random intercept to account for interpersonal variability.
For both the TCT and subjective ratings, we performed likelihood
ratio tests (LRTs) comparing each model to reduced models, in which
we individually dropped one fixed effect or interaction. Where we
found a significant main or interaction effect, we conducted pairwise
post-hoc comparisons with Bonferroni correction.
For qualitative analysis, we first transcribed the post-experiment
interviews and participant comments during the study using Whisper9
and manually corrected transcription inaccuracies. We used thematic
analysis to identify themes in the interviews following the process out-
lined by Blandford et al. [10]. Three researchers independently coded
the same three interviews (= 20%) using an open-coding approach.
These codes were then discussed, and all researchers agreed on a final
set of codes. Finally, one researcher coded all interviews (including
re-coding of the three initial samples) with the defined codes using the
Atlas.ti analysis software10.
4
RESULTS
In the following, we present the results of our experimental user study.
First, we report our quantitative results regarding task completion times
(see section 4.1) and participants’ subjective ratings (4.2) for both
the SHAPE and SIZE conditions. We follow this with the qualitative
findings from the post-experiment interviews (4.3).
4.1
Task Completion Time
For the SHAPE-CONDITION, LRTs revealed a significant main ef-
fect of the SHAPE of the RECEIVER (χ2(6) = 14.70, p < .05) on
task completion times, but no significant effects of the GIVER’s
SHAPE (χ2(6) = 10.68, p = 0.10) or interaction effect between them
(χ2(4) = 6.07, p = 0.19). Post-hoc comparisons on levels of the RE-
CEIVER’s SHAPE show significantly (p < .05) higher completion times
for the Pyramid visualization (M = 41.5s, SD = 13.0s) compared to
Cube (M = 38.7s, SD = 16.0s). The Sphere (M = 38.8s, SD = 8.21s)
performed slightly worse than Cube and better than Pyramid, but these
differences were not significant. Comparing the full model to a reduced
8One participant noted a deficiency in their right eye but confirmed they
could complete the task without difficulty.
9https://github.com/openai/whisper, accessed: 2025-03-10
10https://atlasti.com/, accessed: 2025-03-10
Authorized licensed use limited to: University of Southern Denmark. Downloaded on December 01,2025 at 08:22:43 UTC from IEEE Xplore.  Restrictions apply. 
10113
weiss ET AL.: INVESTIGATING THE EFFECTS OF HAPTIC ILLUSIONS IN COLLABORATIVE VIRTUAL REALITY
Question
VIS
OTHERSVIS
ROLE
V×OV
V×R
OV×R
V×OV×R
χ2
p
χ2
p
χ2
p
χ2
p
χ2
p
χ2
p
χ2
p
SHAPE
Q1
307.57
< .001
7.25
0.84
4.14
0.90
6.06
0.64
2.95
0.82
3.13
0.79
2.64
0.62
Q2
89.69
< .001
6.52
0.89
3.32
0.95
5.37
0.72
3.01
0.81
2.56
0.89
1.95
0.74
Q3
123.07
< .001
10.07
0.61
6.03
0.74
9.20
0.33
4.39
0.62
4.28
0.64
4.10
0.39
Q4
45.53
< .001
8.09
0.78
4.24
0.90
6.85
0.55
1.82
0.94
1.36
0.97
0.74
0.95
Q5
433.05
< .001
3.37
0.99
3.22
0.95
3.12
0.93
2.96
0.81
1.53
0.96
1.51
0.82
Q6
174.68
< .001
3.35
0.99
3.55
0.94
3.19
0.92
3.41
0.76
3.13
0.79
3.07
0.54
SIZE
Q1
185.35
< .001
4.99
0.96
1.82
0.99
4.59
0.80
0.99
0.99
0.98
0.99
0.83
0.93
Q2
54.61
< .001
7.76
0.80
6.57
0.68
3.81
0.87
3.50
0.74
4.48
0.61
1.44
0.84
Q3
76.91
< .001
7.73
0.81
9.42
0.40
5.82
0.67
7.40
0.29
4.59
0.60
4.02
0.40
Q4
16.18
0.18
12.38
0.42
9.69
0.38
9.26
0.32
7.09
0.31
9.20
0.16
6.75
0.15
Q5
179.43
< .001
3.76
0.99
5.98
0.74
3.38
0.91
5.24
0.51
2.42
0.88
2.24
0.69
Q6
94.65
< .001
5.35
0.95
4.43
0.88
4.20
0.84
2.23
0.90
1.26
0.97
1.24
0.87
Table 1: Likelihood Ratio Tests comparing the full models to reduced models with one term or one interaction dropped out. Each column represents
a model comparison, where VIS, OTHERSVIS, and ROLE are main effects, and the remaining columns are interaction effects. For example, VIS
shows the difference between the full model to the model with the term VIS removed, and V×R shows the difference between the full model and a
model with the interaction between VIS and ROLE removed.
one without a trial index as a random intercept, we found a significant
difference in TCT (χ2(1) = 120.59, p < .001), suggesting a strong
learning effect.
For the trials where SIZE was varied (SIZE-CONDITION), the
LRTs revealed a significant main effect of the GIVER’s SIZE (χ2(6) =
17.89, p < .01), RECEIVER’s SIZE (χ2(6) = 22.15, p < .01) and a sig-
nificant interaction effect of GIVER’s and RECEIVER’s SIZE (χ2(4) =
15.94, p < .01). Due to the involvement of interactions, we performed
post-hoc comparisons on all groups of GIVER’s SIZE×RECEIVER’s
SIZE and found the group Matching →Bigger (M = 41.8s, SD = 16.9s)
performing the worst, resulting in significantly (p < .05) higher com-
pletion times than the groups of Matching →Matching (M = 35.9s,
SD = 8.74s) and Bigger →Matching (M = 37.8s, SD = 6.83s). Ad-
ditionally, we again found a learning effect with the LRT showing a
significant difference between the models with and without the trial
index as a random effect (χ2(1) = 81.48, p < .001).
4.2
Subjective Ratings
All Likelihood Ratio Tests comparing the full and reduced models are
shown in Table 1. For the SHAPE-CONDITION, we found a significant
main effect of SHAPE on all subjective ratings, while OTHERSSHAPE,
ROLE, or their interactions did not show any significant differences
when removed from the full model. Rating distributions for levels
of SHAPE averaged over OTHERSSHAPE and ROLE are presented in
Figure 3, with asterisks and brackets indicating significant differences
found among groups with post-hoc comparisons.
For the SIZE-CONDITION, LRTs showed a significant effect of SIZE
for all ratings except Q4 (I think the other person performed their task
well.), and no other main or interaction effect. We present the average
rating distributions in Figure 4. Significant differences among groups
are again highlighted with an asterisk.
In the following, we present the results of post-hoc comparisons
conducted on the main effects of SHAPE and SIZE in their respective
conditions for each subjective rating.
4.2.1
The interaction with the object felt natural.
SHAPE-CONDITION
For participants’ assessments of naturalness
of the interaction, post-hoc tests revealed significant (p < .001) differ-
ences among all levels of SHAPE, with Cube (M = 6.08,SD = 1.01) re-
ceiving the highest ratings, followed by Sphere (M = 4.11,SD = 1.83)
and lastly Pyramid (M = 3.54,SD = 1.79).
SIZE-CONDITION
Analogously, we found significant (p < .001)
differences for all contrasts of SIZE, with Matching (M = 5.96,SD =
1.12) receiving the highest ratings, followed by Bigger (M =
5.28,SD = 1.32) and then Smaller (M = 4.40,SD = 1.61).
4.2.2
I am satisfied with my performance in the task.
SHAPE-CONDITION
Regarding subjective performance, com-
parisons showed significantly (p < .001) higher ratings of Cube
(M = 6.43,SD = 0.67) compared to Sphere (M = 6.08,SD = 0.83) or
Pyramid (M = 5.94,SD = 0.88), but no significant difference between
Sphere and Pyramid.
SIZE-CONDITION
For SIZE, post-hoc tests showed significant
(p < .001) difference between Matching (M = 6.39,SD = 0.69) and
Smaller (M = 6.03,SD = 0.83), as well as Bigger (M = 6.27,SD =
0.73) and Smaller. Additionally. it revealed a significant (p < .05)
difference between Matching and Bigger.
4.2.3
It was easy for me to achieve my level of performance.
SHAPE-CONDITION
Post-hoc comparisons showed significantly
(p < .001) higher ratings of Cube (M = 6.45,SD = 0.71) compared to
Sphere (M = 5.81,SD = 1.26) or Pyramid (M = 5.66,SD = 1.24), but
no significant difference between Sphere and Pyramid.
SIZE-CONDITION
Similarly, we see significant (p < .001) dif-
ferences between Matching (M = 6.43,SD = 0.76) and Smaller (M =
5.92,SD = 1.02), as well as Bigger (M = 6.26,SD = 0.89) and Smaller.
Additionally. we found a significant (p < .01) difference between
Matching and Bigger.
4.2.4
I think the other person performed their task well.
SHAPE-CONDITION
We found significantly (p < .001) higher
performance ratings for Cube (M = 6.54,SD = 0.0.58) compared to
Sphere (M = 6.29,SD = 0.70) or Pyramid (M = 6.28,SD = 0.71). The
ratings for Sphere and Pyramid are not significantly different.
SIZE-CONDITION
For SIZE, we did not compute post-hoc com-
parisons as we found no significant main effect in LRTs.
4.2.5
The visualization of the object matched the physical ob-
ject well.
SHAPE-CONDITION
For subjective ratings of visuo-haptic con-
gruence, post-hoc tests show significantly (p < .001) higher ratings
for Cube (M = 6.04,SD = 1.07) than Sphere (M = 2.84,SD = 1.80) or
Pyramid (M = 1.65,SD = 1.65) but no significant difference between
Sphere and Pyramid.
SIZE-CONDITION
Tests reveal significant (p < .001) differences
among all levels of SIZE, which Cube subjectively matching the best
(M = 5.93,SD = 1.21), followed by the Bigger (M = 5.03,SD = 1.58)
and lastly Smaller (M = 4.11,SD = 1.66) visualizations.
Authorized licensed use limited to: University of Southern Denmark. Downloaded on December 01,2025 at 08:22:43 UTC from IEEE Xplore.  Restrictions apply. 
IEEE TRANSACTIONS ON VISUALIZATION AND COMPUTER GRAPHICS, VOL. 31, NO. 11, NOVEMBER 2025
10114
***
***
***
***
***
***
***
***
***
***
***
***
***
**
4. i think the other person 
 performed their task well.
5. The visualization of the object 
 matched the physical object well.
6. The visualization of the object 
 impaired my performance.
1. The interaction with the 
 object felt natural.
2. i am satisfied with my 
 performance in the task.
3. it was easy for me to achieve 
 my level of performance.
1
2
3
4
5
6
7
1
2
3
4
5
6
7
Agreement with statements from 
1(strongly disagree) to 7(strongly agree)
shape
Cube
sphere
Pyramid
Fig. 3: Agreement ratings for our questionnaire statements in the SHAPE-CONDITION. Box and violin plots depict the distribution of ratings for levels
of SHAPE averaged over all levels of OTHERSSHAPE and ROLE. Brackets and asterisks mark significant pairwise differences between groups found
in post-hoc comparisons (∗= p < .05,∗∗= p < .01,∗∗∗= p < .001).
4.2.6
The visualization of the object impaired my performance.
SHAPE-CONDITION
We found significantly (p < .001) lower rat-
ings for Cube (M = 2.54,SD = 1.92) than Sphere (M = 3.97,SD =
1.63) and Pyramid (M = 4.35,SD = 1.49) and a significant (p < .01)
difference between Sphere and Pyramid.
SIZE-CONDITION
Post-hoc comparisons again showed significant
(p < .001) differences among all levels of SIZE, which Cube causing
the least subjective impairment on performance (M = 2.61,SD = 1.88),
followed by the Bigger (M = 3.06,SD = 1.77) and lastly Smaller (M =
3.76,SD = 1.70) level.
4.3
Qualitative Findings
In this section, we present the qualitative findings derived from the
semi-structured interviews we conducted with participants after the
experiment. We structure them based on the discovered overarching
themes of the shift in the participants’ reliance on different sensory
channels (see section 4.3.1), their resulting strategy changes (4.3.2),
and their perceived adaption to the visuo-haptic and interpersonal in-
congruencies (4.3.3).
4.3.1
Shifting Reliance on Sensory Channels
A common theme among interviewed participants was the shifting
reliance on visual and haptic senses for the interaction. Participants
were more dependent on their sense of touch, especially during the
handover: "I touched the object a couple of seconds, and then I took it
from him so I know that I’m actually getting the object" (P14a). Visual
cues were mainly employed before touching to locate the object and
prepare the strategy for the grasping action: "I was pretty much relying
on what I’m seeing [to determine] where to move or how to or where
to reach or approach an object. But while I’m actually handling it, I
was more trusting my sense of touch" (P06a).
However, this reliance on visual cues before touch caused some
issues when they were not aligning: "It was larger, so, I mean, my
instinct was to hold it more widely" (P03a). These challenges ultimately
caused participants to lower their trust and reliance on vision: "I didn’t
trust what I was seeing. I was just like feeling like a blind person"
(P03b).
4.3.2
Strategies for Attention & Sensorimotor Behavior
Participants developed explicit strategies to handle the handover task.
Regarding their actions, participants noted they generally defaulted
to handing the object over while grasping the sides and receiving it
with their hands underneath. They felt this decreased the likelihood
of dropping the object: "To hand over, it was better to use the sides
and to receive the object, I’ll just go underneath it, so I just cover
the whole area, I won’t drop it" (P06b). Additionally, it could handle
the visuo-haptic discrepancies well, for instance, with the Smaller
visualization: "Yeah, but with the small object, it was easier to grab it
from the bottom" (P10a). Alternative strategies were rarely employed,
though some participants chose the inverse strategy: "I tried to touch
the bottom, so it was more easy for the other person to grab the hands
from both sides" (P13a). After strategies were formed, they remained
mostly unchanged as participants relied on world knowledge: "I would
learn what the actual size of the box was, and whenever I saw that the
visual or the virtual box was bigger, I would have already known that I
trained my hands [...] to grasp a smaller object" (P11b). However, the
selection of which strategy to use was occasionally adjusted based on
the visualization: "From the side, if it’s smaller and if it’s larger then
from the bottom" (P03b).
In the handover task, participants differed in their attentional focus;
some concentrated primarily on the virtual object, while others relied
more on the representations of the other’s hands: "I will also look
at what shape she got, so I already knew, for example, she could
make a larger thing that I have to hold it a little closer" (P03a). "Not
the virtual object. It was more the hands of the other person or my
hands" (P10a). As a rationale for prioritizing the object over the hands,
participants cited inaccuracies in hand tracking reducing their trust in
hand visualizations: "I tried to look at her hands, but I didn’t know if I
could trust them because sometimes they were flying around" (P03b).
Consequently, alternative approaches to identifying the hand positions
without relying on vision were developed: "I was trying to sometimes
feel my partner’s hand because the objects were different than the box"
(P07a). The distribution of participants’ attention was also influenced
by the manipulations: "For the cubes, especially for the big ones, I think
it was easier to focus on the virtual object [...], but with the triangles
[Pyramid], there always I focus on the hands" (P10a).
Authorized licensed use limited to: University of Southern Denmark. Downloaded on December 01,2025 at 08:22:43 UTC from IEEE Xplore.  Restrictions apply. 
10115
weiss ET AL.: INVESTIGATING THE EFFECTS OF HAPTIC ILLUSIONS IN COLLABORATIVE VIRTUAL REALITY
***
***
***
***
*
***
***
***
***
***
**
***
***
***
***
4. i think the other person 
 performed their task well.
5. The visualization of the object 
 matched the physical object well.
6. The visualization of the object 
 impaired my performance.
1. The interaction with the 
 object felt natural.
2. i am satisfied with my 
 performance in the task.
3. it was easy for me to achieve 
 my level of performance.
1
2
3
4
5
6
7
1
2
3
4
5
6
7
Agreement with statements from 
1(strongly disagree) to 7(strongly agree)
size
Matching
Bigger
smaller
Fig. 4: Agreement ratings for our questionnaire statements in the SIZE-CONDITION. Box and violin plots depict the distribution of ratings for levels of
SIZE averaged over all levels of OTHERSSIZE and ROLE. Brackets and asterisks mark significant pairwise differences between groups found in
post-hoc comparisons (∗= p < .05,∗∗= p < .01,∗∗∗= p < .001).
4.3.3
Adapting to Incongruence
While participants did not find overcoming the mismatches overly diffi-
cult, they noted that maintaining their performance required conscious
mental effort: "I always had to remind myself that it’s not a ball"
(P15b). "I had to, you know, switch on my mind every time" (P11a).
"We had to stay alert because we did not know what shape we were
going to handle" (P02b). Hand tracking – even with employed hand
redirection – helped participants link the virtual to the physical object
through proprioceptive, haptic, and visual cues: "You could always see
your hands where it was. So that helped gauge, see how the structure
was in respect to what we were seeing and what the real thing was"
(P12a).
Participants felt they adapted to the interaction with the visual-haptic
discrepancies, making it easier and more natural over time: "We just
got better, and somehow, by the end, it also felt more natural than
at the beginning" (P01b). "Over time I just got used to the point
that maybe the object might not be the same shape as I visualized"
(P06b). However, participants noted this adaption was harder for some
visualizations, especially the Pyramid: "Through the whole experiment
I had problems with the pyramid" (P15b). "The pyramid [...] was the
smallest object and that might have been a bit more difficult [...] to
adapt to" (P11b).
Further, participants perceived the handover to be more challenging
and unsafe than the pick-up from the stationary platform: "Picking
up [...] was very easy, but the handover, even if you’ve done it so
many times, it was always tricky" (P08a). "I think grabbing it from
the starting point, [...] I know it’s in a safe position, I cannot drop it.
But getting it from [the other participant], it was sometimes a little bit
scary" (P09a).
Participants generally did not seem to notice the discrepancies in
visualizations between them and the opposing participant. When in-
formed, the majority reacted surprised, while some mentioned some
suspicions. However, participants believed that these changes did not
affect their strategy: "If I see a cube and he sees a pyramid, he would
still try to grab it from the bottom" (P12a).
5
DISCUSSION
Our results show that visual shape and size manipulations significantly
impacted users’ performance, subjective experience, and strategies.
In the following section, we discuss these findings, structured around
our research questions. We first examine the effects of visuo-haptic
(see section 5.1) and interpersonal incongruence (5.2) on performance
and user experience in a collaborative handover task (RQ1), followed
by an analysis of adaptation strategies (5.3, RQ2). As this is the
first study exploring HIs in a collaborative XR setting, we also derive
practical recommendations and guidelines for integrating these illusions
into such contexts (5.4). Lastly, we discuss the boundaries of our
investigation and propose future research directions that may support
establishing HIs as powerful tools to enrich haptic experiences in future
collaborative XR (5.5).
5.1
Visuo-haptic discrepancies negatively impact users’
performance and experience
Incongruent visuo-haptic representations of shape and size significantly
impacted user performance and subjective experience in this collab-
orative task. This finding is in line with the results of single-user
studies [31,46]. Discrepant visualizations of local surface orientation
(Pyramid), local curvature (Sphere), and volume (Bigger/Smaller)
led to longer completion times and lower subjective ratings, such as
the naturalness of the interaction and participants’ self-assessments
of their own or the other’s performance. Participants reported exert-
ing conscious mental effort to adapt but felt they eventually adjusted,
both cognitively — finding the task more natural over time — and
behaviorally — developing sensorimotor strategies to compensate for
unreliable visual cues. The physical prop was never dropped during
the study, showing that difficulties inherent to incongruent presenta-
tions can be successfully overcome. However, despite learning effects,
which participants reported and we confirmed in TCT, handovers with
incongruent stimuli still exhibited significantly increased completion
times, suggesting participants had to slow down to remain consistent.
Participants consistently rated the Pyramid and Smaller visualiza-
tions as the most demanding in their respective conditions. The lower
ratings and worse performance of Pyramid compared to Sphere sug-
gest that mismatches in local surface orientation were more challenging
to adapt to than local curvature. However, absolute comparisons of the
dimensions of local orientation and local curvature of two primitives
are difficult concerning their objective and subjective magnitude of
discrepancies (e.g, Is a Cube more similar to a Sphere or a Pyramid?;
refer to [46] for findings on the subjective similarity of discrete objects).
Nevertheless, visuo-haptic discrepancies in local orientation have been
shown to be detected earlier (i.e., lower just-noticeable-difference) by
Authorized licensed use limited to: University of Southern Denmark. Downloaded on December 01,2025 at 08:22:43 UTC from IEEE Xplore.  Restrictions apply. 
IEEE TRANSACTIONS ON VISUALIZATION AND COMPUTER GRAPHICS, VOL. 31, NO. 11, NOVEMBER 2025
10116
participants than curvature mismatches [48], and the Pyramid shape
was generally perceived as the least matching to the physical shape by
participants. This heightened sensitivity to discrepancies likely con-
tributed to the reduced performance and subjective ratings for Pyramid.
Additionally, the Pyramid was perceived as the smallest shape, aligning
with its lower overall volume despite a constant base width. This reduc-
tion in volume may have further impacted performance and experience,
analogous to the Smaller level in the SIZE-CONDITION.
Here, the reduction in size was also noticed more and rated as less
matching the actual size than the Bigger condition, which similarly
justifies its worse objective and subjective performance. In AR contexts,
prior research has found size manipulations to generally be less disrup-
tive to user experience than shape changes [31]. While both manipula-
tions had significant main effects, this trend is reflected in our results:
the average differences between matching and manipulated visualiza-
tions were generally less pronounced for size changes—particularly for
Bigger visualizations (see Figure 3 and Figure 4, or subsection 4.2 for
exact group means).
5.2
Effects of interpersonal mismatches can outweigh
visuo-haptic incongruence in asymmetric tasks
In our study, we deliberately manipulated object visualizations at an
individual level, introducing two potential incongruencies: (1) between
physical shape and individual visualization (see subsection 5.1) and (2)
between both participants’ visualizations. Subjective ratings and quali-
tative insights indicate that visuo-haptic incongruence had the greatest
impact, while interpersonal incongruence was less noticed. Comple-
tion times support this, with significant effects observed only when
the Receiver experienced visuo-haptic incongruence for shape manip-
ulations. However, for size manipulations, interpersonal mismatches
significantly affected task completion. Unsurprisingly, Matching sizes
between participants performed best. However, while increasing the
Giver’s size from Matching to Bigger performed nearly as well as
the fully Matching condition, changing the Receiver’s visualization to
Bigger led to the worst performance – even surpassing visuo-haptic
mismatches for both (i.e., Bigger →Bigger or Smaller →Smaller).
This suggests that interpersonal congruence can, in some cases, out-
weigh visuo-haptic congruence, particularly for the Receiver, and that
the individual role of the user plays a key part. This was reinforced
by participants’ interviews, which highlighted the increased difficulty
of receiving due to compounded discrepancies and the dynamic, less
predictable movement of the Giver.
5.3
Users consciously adapt by shifting reliance to touch
but cannot fully compensate for vision
Participants adapted to visuo-haptic mismatches by shifting reliance
from vision to haptic and proprioceptive feedback. This adaptation,
reported by participants and evident in sensorimotor strategies, priori-
tized a stable grasp of the physical cube, largely independent of visual
input. This aligns with the maximum likelihood model of visuo-haptic
integration, where sensory weighting is based on the reciprocal vari-
ance of senses [18]. As visual reliability decreased in our experiment,
perception increasingly depended on haptic information over vision.
This likely originated from the fact that the reliability of sensory infor-
mation in our study was shaped by prior world knowledge. Participants
expected visual inconsistencies while anticipating a static physical
presentation.
Participants felt their adaptation improved object handling over time,
and we observed performance changes across trials, likely reflecting
general learning effects. To account for this in our analysis, we random-
ized condition orders between sessions and included the trial numbers
in our mixed-effects models. Even with these controls, incongruent
visuals still significantly affected performance, suggesting that the ef-
fects observed are robust and would likely be stronger in the absence
of adaptation. This indicates that vision remains crucial for grasp-
ing and cannot be entirely substituted by other sensory modalities.
Studies on visuo-haptic grasping emphasize vision’s critical role in
guiding reach-to-grasp actions, providing reliable location cues (which
remained stable in our study) and size/shape information (which we
manipulated) [11,44]. While learning effects are expected, there is a
distinction between learning the task itself and learning how to cope
with sensory incongruence. Prior work shows that people can adapt
to constant visuo-haptic discrepancies in local orientations [54] or
sizes [53] through fixed sensory-to-motor transformations, enabling
compensation for unreliable vision. However, in our study, object vi-
sualizations changed after each handover, preventing adaptation to a
static discrepancy. As a result, participants had to consciously account
for visual unreliability in every grasp, explaining its negative impact on
performance and strategy despite perceived adaptation.
Regarding potential inter- and intrapersonal factors, prior single-user
investigations have demonstrated individual variability in visuo-haptic
tasks. For instance, age [38] and finger size [41] affect haptic acuity,
and sensitivity to sensory incongruence varies between individuals [22,
32] or when distractions are added [58]. We aimed to mitigate the
influence of these factors by using large, clearly noticeable stimuli
and discrepancies for our investigation. However, regarding contextual
factors, we found that participants’ difficulty in adapting to the HI
was influenced by the user’s individual task (Giver or Receiver) in the
interaction, which indicates that adaptation to mismatches is task- or
role-dependent. This highlights the importance of designing HIs that
account for individual contexts, especially in complex collaborative
tasks, for instance, by personal calibration approaches [21,55].
5.4
Guidelines for the integration of Haptic Illusions into
Collaborative XR
In this work, we explored the challenges of integrating HIs into han-
dover tasks to enhance future haptic experiences in collaborative XR,
which still rely solely on passive props. Based on our quantitative and
qualitative findings, we provide guidelines for researchers and design-
ers to adapt HIs from single-user scenarios to these novel multi-user
contexts.
5.4.1
Design physical props smaller and augment their size
virtually.
Our results show that visuo-haptic mismatches are more disruptive
when the virtual object appears smaller than the physical prop, par-
ticularly for certain shapes like the Pyramid. To minimize perceptual
inconsistencies and allow for dynamic size or shape variations, we rec-
ommend constructing smaller physical objects and using HIs to achieve
the desired larger virtual appearance.
5.4.2
Set more conservative boundaries for visuo-haptic dis-
crepancies.
While prior work in single-user contexts showed that a small set of
physical primitives can support a wide range of virtual shapes and
sizes [31, 46, 47], our findings suggest that this flexibility does not
fully translate to collaborative scenarios. Incongruencies that may be
sufficient for single-user interactions may become disruptive when
multiple users interact with the same prop, as evidenced by reduced
performance and subjective ratings in our study. Consequently, we
recommend applying more conservative limits to visuo-haptic manipu-
lations when transferring findings from single-user investigations. This
may require a broader set of physical primitives to maintain immersion
and performance.
5.4.3
Minimize mismatches for the more demanding role or
synchronize mismatches across users.
Using HIs to extend the capabilities of physical props inherently re-
quires introducing controlled mismatches. We found that users in more
demanding roles – such as the Receiver in handovers – were more af-
fected by these mismatches. However, we found that these mismatches
are less disruptive when they are applied consistently across both users.
Therefore, we recommend reducing mismatches for the more sensitive
role whenever possible or keeping them consistent across both users to
preserve interaction quality.
Authorized licensed use limited to: University of Southern Denmark. Downloaded on December 01,2025 at 08:22:43 UTC from IEEE Xplore.  Restrictions apply. 
10117
weiss ET AL.: INVESTIGATING THE EFFECTS OF HAPTIC ILLUSIONS IN COLLABORATIVE VIRTUAL REALITY
5.4.4
Support adaptation through multimodal cues and prior
knowledge
Our findings reveal strong learning effects during exposure to visuo-
haptic mismatches. Participants used sensorimotor strategies and multi-
modal cues to adapt, for instance, watching or touching their partner’s
hands rather than the object, or comparing visual and proprioceptive
feedback of their own hands. They also leveraged their awareness
that the physical prop remained unchanged to interpret the visual ma-
nipulations more effectively. Providing multimodal cues, integrating
users’ prior world knowledge, and allowing time for strategy formation
can significantly enhance users’ adaptation, performance, and overall
experience.
5.5
Limitations & Future Work
Our study takes an initial step toward integrating HIs into collaborative
XR. However, adapting these phenomena from single-user studies to
complex multi-user settings introduces practical constraints and novel
aspects that a single study cannot fully explore.
First, we deliberately focused our investigation on large, distinctly
noticeable manipulations in visual shape and size to examine the ex-
treme cases of mismatch. HI manipulations near or within their respec-
tive detection thresholds are likely to influence the measured metrics to
a lesser extent. The effect of these elicitations and the transferability of
established detection thresholds to collaborative XR contexts require
further investigation.
Further, we quantified users’ performance based on completion
times, which might not always be the sole critical metric depending on
the XR scenario. Professional training might favor accuracy and safety
over speed. However, to measure accuracy, we would first require
precise knowledge of optimal task completion, which needs to be
determined based on the individual setting requirements. To determine
safety and consistency, failure rates (e.g., how often users drop or
misplace the object) could be investigated, which would, however,
require much more demanding task procedures to induce frequent
errors. Instead, we opted for a broader metric and complemented
this with qualitative insights. Future work might look into alternative
measurements to acquire more target-specific insights.
Additionally, our approach was constrained by current limitations
in hand tracking, which participants frequently found unreliable. We
employed an established optical method shown to be accurate [49],
including through acrylic glass [50]. Nevertheless, these systems still
require direct visibility of the hands and are prone to occlusion errors.
While all conditions were affected, including congruent ones, such
unreliability may impact collaboration strategies, as they introduce
additional incongruence to visuo-proprioceptive interactions, which
warrant their own investigation.
Lastly, our study focused specifically on one core aspect of haptic
feedback in collaborative XR: the handover of a physical object. We
selected this task because it represents a fundamental interaction re-
quiring synchronous coordination with a shared prop. However, many
other scenarios could benefit from passive props and HIs, such as joint
object carrying, physical contact in competitive games [23], collab-
orative assembly [1], or medical training [45]. These settings may
demand more than shape or size manipulations, for instance, HIs that
simulate stiffness [56] and weight [43] to convey material properties or
temperature [24] to produce safe and scalable scenarios. Especially in
training scenarios where accuracy is critical, both the benefits and risks
of applying HIs must be carefully evaluated. As our findings suggest
that collaborative use of HIs can differ significantly from single-user
results, we advocate for future research to extend established HIs to
more ecologically valid contexts.
6
CONCLUSION
Realistic haptic feedback is crucial for physical collaboration but re-
mains challenging in multi-user XR, where systems typically rely on
static passive props. HIs have shown promise in single-user contexts
but are largely unexplored in collaborative scenarios. To investigate
their applicability to these contexts, we conducted a user study examin-
ing the effects of shape and size illusions on performance, experience,
and behavior during a VR handover task.
We found that visual-haptic mismatches disrupted collaboration,
leading to reduced performance and user experience. While users
adapted over time using multisensory cues and sensorimotor strategies,
visual inconsistencies could not be fully compensated. Further, the in-
fluence on performance was affected by users’ roles in the interactions
and the synchrony of individual visualizations. These insights highlight
the need for more careful use of HIs in collaborative XR – by managing
mismatches more conservatively, supporting adaptation through cues
and training, and accounting for role-based differences. With these find-
ings and recommendations, we lay the groundwork for the integration
of HIs into multi-user contexts, supporting the development of richer
and more adaptable haptic experiences for future XR collaboration.
7
OPEN SCIENCE
We provide access to our collected datasets, Unity project, code-set
for the interviews, and data analysis scripts at this link: https://osf.
io/ugnf4/.
ACKNOWLEDGMENTS
This project is funded by the Deutsche Forschungsgemeinschaft
(DFG) - project-id:
521602817 as part of the Priority Pro-
gram SPP2199 ’Scalable Interaction Paradigms for Pervasive Com-
puting Environments’.
This work has been co-funded by the
LOEWE initiative (Hesse, Germany) within the emergenCITY cen-
ter [LOEWE/1/12/519/03/05.001(0016)/72].
REFERENCES
[1] V. H. Andaluz, J. S. Sánchez, C. R. Sánchez, W. X. Quevedo, J. Varela,
J. L. Morales, and G. Cuzco. Multi-user industrial training and educa-
tion environment. In L. T. De Paolis and P. Bourdot, eds., Augmented
Reality, Virtual Reality, and Computer Graphics, pp. 533–546. Springer
International Publishing, Cham, 2018. 2, 9
[2] F. Argelaguet, T. Sato, T. Duval, Y. Kitamura, and A. Lécuyer. Collabo-
rative pseudo-haptics: Two-user stiffness discrimination based on visual
feedback. In M. Auvray and C. Duriez, eds., Haptics: Neuroscience, De-
vices, Modeling, and Applications, pp. 49–54. Springer Berlin Heidelberg,
Berlin, Heidelberg, 2014. 3
[3] J. Auda, U. Gruenefeld, and S. Schneegass. Enabling reusable haptic
props for virtual reality by hand displacement. In Proceedings of Mensch
Und Computer 2021, MuC ’21, p. 412–417. Association for Computing
Machinery, New York, NY, USA, 2021. doi: 10.1145/3473856.3474000
2, 3
[4] M. Azmandian, T. Grechkin, and E. S. Rosenberg. An evaluation of
strategies for two-user redirected walking in shared physical spaces. In
2017 IEEE Virtual Reality (VR), pp. 91–98, 2017. doi: 10.1109/VR.2017.
7892235 2
[5] M. Azmandian, M. Hancock, H. Benko, E. Ofek, and A. D. Wilson. Haptic
retargeting: Dynamic repurposing of passive haptics for enhanced virtual
reality experiences. In Proceedings of the 2016 CHI Conference on Human
Factors in Computing Systems, CHI ’16, p. 1968–1979. Association for
Computing Machinery, New York, NY, USA, 2016. doi: 10.1145/2858036
.2858226 4
[6] Y. Ban, T. Narumi, T. Tanikawa, and M. Hirose. Modifying an identified
position of edged shapes using pseudo-haptic effects. In Proceedings of
the 18th ACM Symposium on Virtual Reality Software and Technology,
VRST ’12, p. 93–96. Association for Computing Machinery, New York,
NY, USA, 2012. doi: 10.1145/2407336.2407353 3
[7] Y. Ban, T. Narumi, T. Tanikawa, and M. Hirose. Displaying shapes with
various types of surfaces using visuo-haptic interaction. In Proceedings
of the 20th ACM Symposium on Virtual Reality Software and Technology,
VRST ’14, p. 191–196. Association for Computing Machinery, New York,
NY, USA, 2014. doi: 10.1145/2671015.2671028 3
[8] D. Bates, M. Mächler, B. Bolker, and S. Walker. Fitting linear mixed-
effects models using lme4. Journal of Statistical Software, 67(1):1–48,
2015. doi: 10.18637/jss.v067.i01 4
[9] H. Benko, C. Holz, M. Sinclair, and E. Ofek. Normaltouch and texture-
touch: High-fidelity 3d haptic shape rendering on handheld virtual reality
controllers. In Proceedings of the 29th Annual Symposium on User In-
terface Software and Technology, UIST ’16, p. 717–728. Association for
Authorized licensed use limited to: University of Southern Denmark. Downloaded on December 01,2025 at 08:22:43 UTC from IEEE Xplore.  Restrictions apply. 
IEEE TRANSACTIONS ON VISUALIZATION AND COMPUTER GRAPHICS, VOL. 31, NO. 11, NOVEMBER 2025
10118
Computing Machinery, New York, NY, USA, 2016. doi: 10.1145/2984511
.2984526 1, 2
[10] A. Blandford, D. Furniss, and S. Makri. Qualitative HCI Research: Going
Behind the Scenes. Synthesis Lectures on Human-Centered Informatics,
9(1):1–115, Apr. 2016. doi: 10.2200/S00706ED1V01Y201602HCI034 4
[11] I. Camponogara and R. Volcic. Integration of haptics and vision in human
multisensory grasping. Cortex, 135:173–185, 2021. doi: 10.1016/j.cortex.
2020.11.012 8
[12] A. D. Cheok, X. Yang, Z. Z. Ying, M. Billinghurst, and H. Kato. Touch-
space: Mixed reality game space based on ubiquitous, tangible, and social
computing. Personal and Ubiquitous Computing, 6(5):430–442, Dec 2002.
doi: 10.1007/s007790200047 1, 2
[13] I. Choi, E. Ofek, H. Benko, M. Sinclair, and C. Holz. Claw: A multifunc-
tional handheld haptic controller for grasping, touching, and triggering in
virtual reality. In Proceedings of the 2018 CHI Conference on Human Fac-
tors in Computing Systems, CHI ’18, p. 1–13. Association for Computing
Machinery, New York, NY, USA, 2018. doi: 10.1145/3173574.3174228
1, 2
[14] R. H. B. Christensen. ordinal—Regression Models for Ordinal Data, 2023.
R package version 2023.12-4.1. 4
[15] T. Dong, Y. Shen, T. Gao, and J. Fan. Dynamic density-based redirected
walking towards multi-user virtual environments. In 2021 IEEE Virtual
Reality and 3D User Interfaces (VR), pp. 626–634, 2021. doi: 10.1109/
VR50410.2021.00088 2
[16] Z.-C. Dong, X.-M. Fu, Z. Yang, and L. Liu. Redirected smooth mappings
for multiuser real walking in virtual reality. ACM Trans. Graph., 38(5),
Oct. 2019. doi: 10.1145/3345554 2
[17] B. Ens, J. Lanir, A. Tang, S. Bateman, G. Lee, T. Piumsomboon, and
M. Billinghurst. Revisiting collaboration through mixed reality: The
evolution of groupware. International Journal of Human-Computer Stud-
ies, 131:81–98, 2019. 50 years of the International Journal of Human-
Computer Studies. Reflections on the past, present and future of human-
centred technologies. doi: 10.1016/j.ijhcs.2019.05.011 2
[18] M. O. Ernst and M. S. Banks. Humans integrate visual and haptic infor-
mation in a statistically optimal fashion. Nature, 415(6870):429–433, Jan
2002. doi: 10.1038/415429a 8
[19] M. O. Ernst and H. H. Bülthoff. Merging the senses into a robust percept.
Trends in Cognitive Sciences, 8(4):162–169, 2004. doi: 10.1016/j.tics.
2004.02.002 2
[20] R. Etzi, F. Ferrise, M. Bordegoni, M. Zampini, and A. Gallace. The effect
of visual and auditory information on the perception of pleasantness and
roughness of virtual surfaces. Multisensory Research, 31(6):501–522,
2018. doi: 10.1163/22134808-00002603 2
[21] M. Feick, K. P. Regitz, L. Gehrke, A. Zenner, A. Tang, T. P. Jungbluth,
M. Rekrut, and A. Krüger. Predicting the limits: Tailoring unnotice-
able hand redirection offsets in virtual reality to individuals’ perceptual
boundaries. In Proceedings of the 37th Annual ACM Symposium on User
Interface Software and Technology, UIST ’24. Association for Computing
Machinery, New York, NY, USA, 2024. doi: 10.1145/3654777.3676425 8
[22] M. Feick, K. P. Regitz, A. Tang, and A. Krüger. Designing visuo-haptic
illusions with proxies in virtual reality: Exploration of grasp, movement
trajectory and object mass. In Proceedings of the 2022 CHI Conference
on Human Factors in Computing Systems, CHI ’22. Association for Com-
puting Machinery, New York, NY, USA, 2022. doi: 10.1145/3491102.
3517671 8
[23] J. Gugenheimer, E. Stemasov, J. Frommel, and E. Rukzio. Sharevr: En-
abling co-located experiences for virtual reality between hmd and non-hmd
users. In Proceedings of the 2017 CHI Conference on Human Factors in
Computing Systems, CHI ’17, p. 4021–4033. Association for Computing
Machinery, New York, NY, USA, 2017. doi: 10.1145/3025453.3025683
2, 9
[24] S. Günther, F. Müller, D. Schön, O. Elmoghazy, M. Mühlhäuser, and
M. Schmitz. Therminator: Understanding the interdependency of visual
and on-body thermal feedback in virtual reality. In Proceedings of the
2020 CHI Conference on Human Factors in Computing Systems, CHI ’20,
p. 1–14. Association for Computing Machinery, New York, NY, USA,
2020. doi: 10.1145/3313831.3376195 2, 9
[25] D.-N. T. Huynh, K. Raveendran, Y. Xu, K. Spreen, and B. MacIntyre. Art
of defense: a collaborative handheld augmented reality board game. In
Proceedings of the 2009 ACM SIGGRAPH Symposium on Video Games,
Sandbox ’09, p. 135–142. Association for Computing Machinery, New
York, NY, USA, 2009. doi: 10.1145/1581073.1581095 1, 2
[26] B. E. Insko.
Passive haptics significantly enhances virtual environ-
ments. PhD thesis, The University of North Carolina at Chapel Hill,
2001. AAI3007820. 2
[27] L. Jones. Haptics. The MIT Press, 2018. 2
[28] L. A. Jones and S. J. Lederman. Human Hand Function. Oxford Univer-
sity Press, Oxford, United Kingdom, 05 2006. doi: 10.1093/acprof:oso/
9780195173154.001.0001 1, 2
[29] B. Knoerlein, G. Székely, and M. Harders. Visuo-haptic collaborative aug-
mented reality ping-pong. In Proceedings of the International Conference
on Advances in Computer Entertainment Technology, ACE ’07, p. 91–94.
Association for Computing Machinery, New York, NY, USA, 2007. doi:
10.1145/1255047.1255065 2
[30] M. Kurzweg, Y. Weiss, M. O. Ernst, A. Schmidt, and K. Wolf. Survey on
haptic feedback through sensory illusions in interactive systems. ACM
Comput. Surv., 56(8), Apr. 2024. doi: 10.1145/3648353 2
[31] E. Kwon, G. J. Kim, and S. Lee. Effects of sizes and shapes of props in
tangible augmented reality. In 2009 8th IEEE International Symposium on
Mixed and Augmented Reality, pp. 201–202, 2009. doi: 10.1109/ISMAR.
2009.5336463 2, 7, 8
[32] A. Lecuyer, J.-M. Burkhardt, S. Coquillart, and P. Coiffet. "boundary
of illusion": an experiment of sensory integration with a pseudo-haptic
system. In Proceedings IEEE Virtual Reality 2001, pp. 115–122, 2001.
doi: 10.1109/VR.2001.913777 8
[33] A. Lefebvre and A. Pusch. Object deformation illusion on a tactilely
enhanced large tabletop device. In 2012 IEEE VR Workshop on Perceptual
Illusions in Virtual Environments, pp. 27–28, 2012. doi: 10.1109/PIVE.
2012.6229797 3
[34] S. Martin and N. Hillier. Characterisation of the novint falcon haptic
device for application as a robot manipulator. In Australasian Conference
on Robotics and Automation (ACRA), pp. 291–292. Citeseer, Australian
Robotics and Automation Association, Sydney, Australia, 2009. 1, 2
[35] T. H. Massie, J. K. Salisbury, et al. The phantom haptic interface: A
device for probing virtual objects. In Proceedings of the ASME winter
annual meeting, symposium on haptic interfaces for virtual environment
and teleoperator systems, vol. 55, pp. 295–300. Chicago, IL, 1994. 1, 2
[36] V. R. Mercado, M. Marchal, and A. Lecuyer. "haptics on-demand": A
survey on encountered-type haptic displays. IEEE Transactions on Haptics,
PP:1–1, 2021. doi: 10.1109/TOH.2021.3061150 2
[37] D.-H. Min, D.-Y. Lee, Y.-H. Cho, and I.-K. Lee. Shaking hands in virtual
space: Recovery in redirected walking for direct interaction between two
users. In 2020 IEEE Conference on Virtual Reality and 3D User Interfaces
(VR), pp. 164–173, 2020. doi: 10.1109/VR46266.2020.00035 2
[38] J. F. Norman, J. M. Dukes, and T. N. Palmore.
Aging and haptic
shape discrimination: the effects of variations in size. Scientific Reports,
10(1):14690, Sep 2020. doi: 10.1038/s41598-020-71894-y 8
[39] T. Ohshima, K. Satoh, H. Yamamoto, and H. Tamura. Ar2 hockey: A
case study of collaborative augmented reality. In Proceedings of the
Virtual Reality Annual International Symposium, VRAIS ’98, p. 268.
IEEE Computer Society, USA, 1998. 2
[40] C. Pacchierotti, S. Sinclair, M. Solazzi, A. Frisoli, V. Hayward, and D. Prat-
tichizzo. Wearable haptic systems for the fingertip and the hand: Taxon-
omy, review, and perspectives. IEEE Transactions on Haptics, 10(4):580–
600, 2017. doi: 10.1109/TOH.2017.2689006 1, 2
[41] R. M. Peters, E. Hackeman, and D. Goldreich. Diminutive digits discern
delicate details: Fingertip size and the sex difference in tactile spatial
acuity. Journal of Neuroscience, 29(50):15756–15761, 2009. doi: 10.
1523/JNEUROSCI.3684-09.2009 8
[42] H. T. Regenbrecht, M. Wagner, and G. Baratoff. Magicmeeting: A collab-
orative tangible augmented reality system. Virtual Reality, 6(3):151–166,
Oct 2002. doi: 10.1007/s100550200016 2
[43] M. Samad, E. Gatti, A. Hermes, H. Benko, and C. Parise. Pseudo-haptic
weight: Changing the perceived weight of virtual objects by manipulating
control-display ratio. In Proceedings of the 2019 CHI Conference on
Human Factors in Computing Systems, CHI ’19, p. 1–13. Association for
Computing Machinery, New York, NY, USA, 2019. doi: 10.1145/3290605
.3300550 2, 9
[44] L. F. Schettino, S. V. Adamovich, and H. Poizner. Effects of object shape
and visual feedback on hand configuration during grasping. Experimental
Brain Research, 151(2):158–166, Jul 2003. doi: 10.1007/s00221-003
-1435-3 8
[45] J. Schild, S. Misztal, B. Roth, L. Flock, T. Luiz, D. Lerner, M. Herkersdorf,
K. Weaner, M. Neuberaer, A. Franke, C. Kemp, J. Pranqhofer, S. Seele,
H. Buhler, and R. Herpers. Applying multi-user virtual reality to collab-
orative medical training. In 2018 IEEE Conference on Virtual Reality
Authorized licensed use limited to: University of Southern Denmark. Downloaded on December 01,2025 at 08:22:43 UTC from IEEE Xplore.  Restrictions apply. 
10119
weiss ET AL.: INVESTIGATING THE EFFECTS OF HAPTIC ILLUSIONS IN COLLABORATIVE VIRTUAL REALITY
and 3D User Interfaces (VR), pp. 775–776, 2018. doi: 10.1109/VR.2018.
8446160 2, 9
[46] A. L. Simeone, E. Velloso, and H. Gellersen. Substitutional reality: Using
the physical environment to design virtual reality experiences. In Proceed-
ings of the 33rd Annual ACM Conference on Human Factors in Computing
Systems, CHI ’15, p. 3307–3316. Association for Computing Machinery,
New York, NY, USA, 2015. doi: 10.1145/2702123.2702389 2, 7, 8
[47] S.-Y. Teng, T.-S. Kuo, C. Wang, C.-h. Chiang, D.-Y. Huang, L. Chan, and
B.-Y. Chen. Pupop: Pop-up prop on palm for virtual reality. In Proceedings
of the 31st Annual ACM Symposium on User Interface Software and
Technology, UIST ’18, p. 5–17. Association for Computing Machinery,
New York, NY, USA, 2018. doi: 10.1145/3242587.3242628 2, 8
[48] X. d. Tinguy, C. Pacchierotti, M. Emily, M. Chevalier, A. Guignardat,
M. Guillaudeux, C. Six, A. Lécuyer, and M. Marchal. How different
tangible and virtual objects can be while still feeling the same? In 2019
IEEE World Haptics Conference (WHC), pp. 580–585, 2019. doi: 10.
1109/WHC.2019.8816164 2, 3, 8
[49] M. Tölgyessy, M. Dekan, J. Rodina, and F. Duchoˇn. Analysis of the leap
motion controller workspace for hri gesture applications. Applied Sciences,
13(2), 2023. doi: 10.3390/app13020742 9
[50] P. P. Valentini and E. Pezzuti. Accuracy in fingertip tracking using leap
motion controller for interactive virtual applications. International Journal
on Interactive Design and Manufacturing (IJIDeM), 11(3):641–650, Aug
2017. doi: 10.1007/s12008-016-0339-y 9
[51] S. Van Damme, F. Van de Velde, M. J. Sameri, F. De Turck, and M. T.
Vega. A haptic-enabled, distributed and networked immersive system
for multi-user collaborative virtual reality. In Proceedings of the 2nd
International Workshop on Interactive EXtended Reality, IXR ’23, p.
11–19. Association for Computing Machinery, New York, NY, USA, 2023.
doi: 10.1145/3607546.3616804 2
[52] C. Wee, K. M. Yap, and W. N. Lim. Haptic Interfaces for Virtual Reality:
Challenges and Research Directions. IEEE Access, 9:112145–112162,
2021. Conference Name: IEEE Access. doi: 10.1109/ACCESS.2021.
3103598 2
[53] C. Weigelt and O. Bock. Adaptation of grasping responses to distorted
object size and orientation. Exp Brain Res, 181(1):139–146, Mar. 2007. 8
[54] C. Weigelt and O. Bock. Adaptation of the precision grip orientation to a
visual-haptic mismatch. Exp Brain Res, 201(4):621–630, Dec. 2009. 8
[55] Y. Weiss, A. Schmidt, and S. Villa. Electrophysiological correlates for
the detection of haptic illusions. IEEE Transactions on Haptics, pp. 1–14,
2025. doi: 10.1109/TOH.2025.3578076 8
[56] Y. Weiss, S. Villa, A. Schmidt, S. Mayer, and F. Müller. Using pseudo-
stiffness to enrich the haptic experience in virtual reality. In Proceedings
of the 2023 CHI Conference on Human Factors in Computing Systems,
CHI ’23. Association for Computing Machinery, New York, NY, USA,
2023. doi: 10.1145/3544548.3581223 2, 9
[57] M. White, J. Gain, U. Vimont, and D. Lochner. The case for haptic props:
Shape, weight and vibro-tactile feedback. In Proceedings of the 12th ACM
SIGGRAPH Conference on Motion, Interaction and Games, MIG ’19.
Association for Computing Machinery, New York, NY, USA, 2019. doi:
10.1145/3359566.3360058 2
[58] A. Zenner and A. Krüger. Estimating detection thresholds for desktop-
scale hand redirection in virtual reality. In 2019 IEEE Conference on
Virtual Reality and 3D User Interfaces (VR), pp. 47–55. IEEE, New York,
NY, USA, 2019. doi: 10.1109/VR.2019.8798143 4, 8
Authorized licensed use limited to: University of Southern Denmark. Downloaded on December 01,2025 at 08:22:43 UTC from IEEE Xplore.  Restrictions apply. 
